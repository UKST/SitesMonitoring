using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SitesMonitoring.DAL;

namespace SitesMonitoring.API.IntegrationTests
{
    public class InMemoryDbEndpointStartup : Startup
    {
        public InMemoryDbEndpointStartup(IHostEnvironment env) : base(env)
        {
        }

        protected override void AddControllers(IServiceCollection services)
        {
            services.AddControllers().AddApplicationPart(typeof(Startup).Assembly);
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