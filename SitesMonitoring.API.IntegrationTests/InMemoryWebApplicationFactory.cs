using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SitesMonitoring.DAL;

namespace SitesMonitoring.API.IntegrationTests
{
    public class InMemoryWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup: class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType ==
                         typeof(DbContextOptions<SitesMonitoringDbContext>));

                services.Remove(descriptor);

                services
                    .AddEntityFrameworkInMemoryDatabase()
                    .AddDbContext<SitesMonitoringDbContext>((serviceProvider, options) =>
                    {
                        options.UseInMemoryDatabase("InMemoryIntegrationTestsDatabase")
                            .UseInternalServiceProvider(serviceProvider);
                    });
            });
        }
    }
}