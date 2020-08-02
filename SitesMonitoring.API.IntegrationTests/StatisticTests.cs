using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using FluentAssertions;
using FluentAssertions.Execution;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using SitesMonitoring.API.Models.Statistics;
using SitesMonitoring.BLL.Monitoring;
using SitesMonitoring.BLL.Monitoring.PingMonitoringAPI;
using SitesMonitoring.BLL.Monitoring.SitesAPI;
using SitesMonitoring.DAL;
using SitesMonitoring.Utils.Http;
using Xunit;

namespace SitesMonitoring.API.IntegrationTests
{
    public class StatisticTests
    {
        private const string SiteName = "Site";
        private const string EndpointUrl = "api/statistics";

        private readonly WebApplicationFactory<Startup> _webApplicationFactory;

        public StatisticTests()
        {
            _webApplicationFactory = new InMemoryWebApplicationFactory<Startup>();
        }

        [Fact]
        public async Task ShouldReturnStatistic_WhenGetRequest()
        {
            // Arrange
            var client = _webApplicationFactory.CreateClient();
            var dbContext = _webApplicationFactory.Services.GetService<SitesMonitoringDbContext>();

            var site = CreateSiteWithMonitoringResult();

            await dbContext.Sites.AddAsync(site);
            await dbContext.SaveChangesAsync();

            // Act
            var response = await client.GetAsync(EndpointUrl);

            // Assert
            using (new AssertionScope())
            {
                response.StatusCode.Should().Be(HttpStatusCode.OK);

                var result = await response.ReadAsync<ICollection<SiteStatisticResultModel>>();
                result.Should().HaveCount(1);
                result.FirstOrDefault()?.SiteName.Should().Be(SiteName);
                result.FirstOrDefault()?.SiteHealth.Should().Be(SiteHealth.Good);
            }
        }

        private static Site CreateSiteWithMonitoringResult()
        {
            return new Site
            {
                Name = SiteName,
                MonitoringEntities = new[]
                {
                    new MonitoringEntity
                    {
                        Type = MonitoringType.Ping,
                        MonitoringResults = new[]
                        {
                            new MonitoringResult
                            {
                                Data = JsonConvert.SerializeObject(new PingMonitoringResultData(IPStatus.Success))
                            }
                        }
                    }
                }
            };
        }
    }
}