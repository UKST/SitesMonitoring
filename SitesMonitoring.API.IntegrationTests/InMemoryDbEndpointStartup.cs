using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SitesMonitoring.DAL;

namespace SitesMonitoring.API.IntegrationTests
{
    public class InMemoryDbEndpointStartup : Startup
    {
        public InMemoryDbEndpointStartup(IConfiguration configuration) : base(configuration)
        {
        }

        protected override void ConfigureMvc(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                .AddApplicationPart(typeof(Startup).Assembly);
        }

        protected override void ConfigureDb(IServiceCollection services)
        {
            services
                .AddEntityFrameworkInMemoryDatabase()
                .AddDbContext<SitesMonitoringDbContext>((serviceProvider, options) =>
                {
                    options.UseInMemoryDatabase("IntegrationTestsDatabase")
                        .UseInternalServiceProvider(serviceProvider);
                });
        }
    }
}