using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using NUnit.Framework;

namespace SitesMonitoring.API.IntegrationTests
{
    public class PingMonitoringTests
    {
        private const string EndpointUrl = "api/sites/1/pingmonitoring";
        private const string AuthenticationScheme = "Basic";
        private const string AuthenticationParameter = "YWRtaW46cGFzc3dvcmQ=";
        
        private readonly TestServer _server;

        public PingMonitoringTests()
        {
            _server = new TestServer(new WebHostBuilder()
                .UseStartup<InMemoryDbEndpointStartup>());
        }

        [Test]
        public async Task GetAll_NoCredentialsProvided_UnauthorizedResponse()
        {
            // Arrange
            var client = GetUnauthenticatedClient();
            
            // Act
            var response = await client.GetAsync(EndpointUrl);
            
            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }
        
        [Test]
        public async Task GetAll_ValidCredentialsProvidedButSiteNotExist_NotFoundResponse()
        {
            // Arrange
            var client = GetAuthenticatedClient();

            // Act
            var response = await client.GetAsync(EndpointUrl);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
        
        // todo other tests
        
        private HttpClient GetUnauthenticatedClient()
        {
            return _server.CreateClient();
        }
        
        private HttpClient GetAuthenticatedClient()
        {
            var client = _server.CreateClient();
            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue(AuthenticationScheme, AuthenticationParameter);

            return client;
        }
    }
}