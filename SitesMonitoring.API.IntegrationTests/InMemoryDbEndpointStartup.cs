using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SitesMonitoring.DAL;

namespace SitesMonitoring.API.IntegrationTests
{
    public class InMemoryDbEndpointStartup : Startup
    {
        public InMemoryDbEndpointStartup(IHostingEnvironment env) : base(env)
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

        protected override void MigrateDatabaseOnStartup(IApplicationBuilder app)
        {
        }
    }
}