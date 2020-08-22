using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Polly;
using SitesMonitoring.API.Composition;
using SitesMonitoring.API.HostedServices;
using SitesMonitoring.API.Mapping;
using SitesMonitoring.API.Models;
using SitesMonitoring.BLL.Configs;
using SitesMonitoring.DAL;

namespace SitesMonitoring.API
{
    public class Startup
    {
        public Startup(IHostEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<AppConfig>(Configuration.GetSection("AppConfig"));

            services.AddLogging();

            services.AddControllers();

            services.AddHostedService<MonitoringHostedService>();

            ConfigureMapping(services);

            services.AddDbContext<SitesMonitoringDbContext>();
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule<CompositionRoot>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostEnvironment env, ILogger<Startup> logger)
        {
            logger.LogInformation("Configuration started");

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseExceptionHandler(errorApp => { errorApp.Run(HandleErrors); });

            app.UseRouting();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

            logger.LogInformation("Db migration executing...");

            MigrateDatabaseOnStartup(app, logger);

            logger.LogInformation("Configuration finished");
        }

        private static void MigrateDatabaseOnStartup(IApplicationBuilder app, ILogger<Startup> logger)
        {
            const int retryAmount = 10;

            Policy
                .Handle<Exception>()
                .WaitAndRetry(
                    retryAmount,
                    retryAttempt => TimeSpan.FromSeconds(retryAttempt),
                    (exception, span) =>
                    {
                        logger.LogError(exception, "Error during database migration");
                    })
                .Execute(() =>
                {
                    logger.LogInformation("Try to migrate database");

                    using var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope();
                    var context = serviceScope.ServiceProvider.GetRequiredService<SitesMonitoringDbContext>();

                    if (context.Database.IsNpgsql())
                    {
                        context.Database.Migrate();
                    }
                });
        }

        private static void ConfigureMapping(IServiceCollection services)
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            var mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
        }

        private static async Task HandleErrors(HttpContext context)
        {
            context.Response.ContentType = "application/json";

            var feature = context.Features.Get<IExceptionHandlerPathFeature>();
            var exception = feature.Error;

            switch (exception)
            {
                case ValidationException validationException:
                    await CreateErrorResult(
                        context,
                        400,
                        validationException);
                    break;
                default:
                    await CreateErrorResult(context, 500, "Internal server error");
                    break;
            }
        }

        private static async Task CreateErrorResult(HttpContext context, int statusCode, ValidationException exception)
        {
            context.Response.StatusCode = statusCode;

            var result = JsonConvert.SerializeObject(
                new PropertyErrorsResultModel
                {
                    PropertyErrors = exception.Errors
                        .GroupBy(i => i.PropertyName)
                        .ToDictionary(
                            k => k.Key,
                            v => v.Select(i => i.ErrorMessage))
                });

            await context.Response.WriteAsync(result);
        }

        private static async Task CreateErrorResult(HttpContext context, int statusCode, params string[] errorMessages)
        {
            context.Response.StatusCode = statusCode;

            var result = JsonConvert.SerializeObject(
                new GenericErrorsResultModel {Errors = errorMessages});

            await context.Response.WriteAsync(result);
        }
    }
}
