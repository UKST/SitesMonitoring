using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Autofac.Extensions.DependencyInjection;
using FluentAssertions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Hosting;
using NUnit.Framework;

namespace SitesMonitoring.API.IntegrationTests
{
    public class PingMonitoringTests
    {
        private const string EndpointUrl = "api/sites/1/pingmonitoring";

        private TestServer _server;

        [OneTimeSetUp]
        public async Task SetUp()
        {
            var host = await new HostBuilder()
                .UseServiceProviderFactory(new AutofacServiceProviderFactory())
                .ConfigureWebHost(webBuilder =>
                {
                    webBuilder
                        .UseTestServer()
                        .UseStartup<InMemoryDbEndpointStartup>();
                })
                .StartAsync();

            _server = host.GetTestServer();
        }

        [Test]
        public async Task GetAll_SiteNotExist_NotFoundResponse()
        {
            // Arrange
            var client = _server.CreateClient();

            // Act
            var response = await client.GetAsync(EndpointUrl);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        // todo other tests
    }
}