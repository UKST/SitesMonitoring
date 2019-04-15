using System;
using System.IO;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using SitesMonitoring.API.Authentication;
using SitesMonitoring.API.Composition;
using SitesMonitoring.API.HostedServices;
using SitesMonitoring.API.Mapping;
using SitesMonitoring.API.Models;
using SitesMonitoring.BLL.ErrorHandling;

namespace SitesMonitoring.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            const string basicAuthentication = "BasicAuthentication";
            services.AddAuthentication(basicAuthentication)
                .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>(basicAuthentication, null);
            
            services.AddHostedService<MonitoringHostedService>();
            
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            var mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

            var containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterModule<CompositionRoot>();
            containerBuilder.Populate(services);
            var container = containerBuilder.Build();
            return new AutofacServiceProvider(container);
        }
        
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseExceptionHandler(errorApp => { errorApp.Run(HandleErrors); });
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseMvc();
        }

        private static async Task HandleErrors(HttpContext context)
        {
            context.Response.ContentType = "application/json";

            var feature = context.Features.Get<IExceptionHandlerPathFeature>();
            var exception = feature.Error;

            switch (exception)
            {
                case ApplicationValidationException _:
                    await CreateErrorResult(context, exception.Message, 400);
                    break;
                case NotFoundException _:
                    CreateErrorResult(context, 404);
                    break;
                default:
                    await CreateErrorResult(context, "Internal server error", 500);
                    break;
            } 
        }

        private static async Task CreateErrorResult(HttpContext context, string errorMessage, int statusCode)
        {
            context.Response.StatusCode = statusCode;

            var result = JsonConvert.SerializeObject(
                new GenericErrorsResultModel {Errors = new[] {errorMessage}});
                    
            await context.Response.WriteAsync(result);
        }
        
        private static void CreateErrorResult(HttpContext context, int statusCode)
        {
            context.Response.StatusCode = statusCode;
        }
    }
}
