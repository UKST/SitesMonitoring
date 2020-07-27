using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using FluentAssertions.Execution;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SitesMonitoring.API.Models.Sites;
using SitesMonitoring.DAL;
using SitesMonitoring.Utils.Http;
using Xunit;

namespace SitesMonitoring.API.IntegrationTests
{
    public class SiteTests
    {
        private const string SiteName = "site";
        private const string EndpointUrl = "api/sites";

        private readonly WebApplicationFactory<Startup> _webApplicationFactory;

        public SiteTests()
        {
            _webApplicationFactory = new InMemoryWebApplicationFactory<Startup>();
        }

        [Fact]
        public async Task ShouldCreateSiteEntity_WhenPostRequest()
        {
            // Arrange
            var client = _webApplicationFactory.CreateClient();
            var dbContext = _webApplicationFactory.Services.GetService<SitesMonitoringDbContext>();

            // Act
            var response = await client.SendAsync(
                new HttpRequestMessage(HttpMethod.Post, EndpointUrl)
                    .FromObject(
                        new SitePostModel
                        {
                            Name = SiteName
                        }));

            // Assert
            using (new AssertionScope())
            {
                response.StatusCode.Should().Be(HttpStatusCode.OK);

                var result = await response.ReadAsync<SiteResultModel>();
                result.Name.Should().Be(SiteName);

                var site = await dbContext.Sites.FirstOrDefaultAsync(i => i.Id == result.Id);
                site.Name.Should().Be(SiteName);
            }
        }
    }
}