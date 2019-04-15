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
                .UseStartup<Startup>());
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
        public async Task GetAll_ValidCredentialsProvided_OkResponse()
        {
            // Arrange
            var client = GetAuthenticatedClient();

            // Act
            var response = await client.GetAsync(EndpointUrl);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
        
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