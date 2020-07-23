using System;
using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace SitesMonitoring.API.IntegrationTests
{
    public class PingMonitoringTests : IDisposable
    {
        private const string EndpointUrl = "api/sites/1/pingmonitoring";

        private readonly WebApplicationFactory<Startup> _webApplicationFactory;

        public PingMonitoringTests()
        {
            _webApplicationFactory = new InMemoryWebApplicationFactory<Startup>();
        }

        public void Dispose()
        {
            _webApplicationFactory.Dispose();
        }

        [Fact]
        public async Task GetAll_SiteNotExist_NotFoundResponse()
        {
            // Arrange
            var client = _webApplicationFactory.CreateClient();

            // Act
            var response = await client.GetAsync(EndpointUrl);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        // todo other tests
    }
}