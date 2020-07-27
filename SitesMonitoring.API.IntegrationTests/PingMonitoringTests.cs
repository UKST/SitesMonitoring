using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using FluentAssertions.Execution;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SitesMonitoring.API.Models.PingMonitoring;
using SitesMonitoring.BLL.Monitoring;
using SitesMonitoring.BLL.Monitoring.SitesAPI;
using SitesMonitoring.DAL;
using SitesMonitoring.Utils.Http;
using Xunit;

namespace SitesMonitoring.API.IntegrationTests
{
    public class PingMonitoringTests : IDisposable
    {
        private const string MonitoringEntityAddress = "site.com";
        private const int PeriodInMinutes = 1;

        private static string EndpointUrl(long sitesId) => $"api/sites/{sitesId}/pingmonitoring";

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
        public async Task ShouldReturnNotFoundResponse_WhenGetRequestForNotExistingSite()
        {
            // Arrange
            var client = _webApplicationFactory.CreateClient();

            // Act
            var response = await client.GetAsync(EndpointUrl(1));

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task ShouldReturnMonitoringEntities_WhenGetRequest()
        {
            // Arrange
            var client = _webApplicationFactory.CreateClient();
            var dbContext = _webApplicationFactory.Services.GetService<SitesMonitoringDbContext>();

            var site = CreateSiteWithMonitoringEntity();
            var monitoringEntity = site.MonitoringEntities.Single();

            await dbContext.Sites.AddAsync(site);
            await dbContext.SaveChangesAsync();

            var expectedMonitoringEntities = new List<PingMonitoringEntityResultModel>
            {
                new PingMonitoringEntityResultModel
                {
                    Id = monitoringEntity.Id,
                    PeriodInMinutes = monitoringEntity.PeriodInMinutes,
                    Address = MonitoringEntityAddress
                }
            };

            // Act
            var response = await client.GetAsync(EndpointUrl(site.Id));

            // Assert
            using (new AssertionScope())
            {
                response.StatusCode.Should().Be(HttpStatusCode.OK);
                var result = await response.ReadAsync<ICollection<PingMonitoringEntityResultModel>>();
                result.Should().BeEquivalentTo(expectedMonitoringEntities);
            }
        }

        [Fact]
        public async Task ShouldCreateMonitoringEntity_WhenPostRequest()
        {
            // Arrange
            var client = _webApplicationFactory.CreateClient();
            var dbContext = _webApplicationFactory.Services.GetService<SitesMonitoringDbContext>();

            var site = new Site();

            await dbContext.Sites.AddAsync(site);
            await dbContext.SaveChangesAsync();

            // Act
            var response = await client.SendAsync(
                new HttpRequestMessage(HttpMethod.Post, EndpointUrl(site.Id))
                    .FromObject(
                        new PingMonitoringEntityPostModel
                        {
                            PeriodInMinutes = PeriodInMinutes,
                            Address = MonitoringEntityAddress
                        }));

            // Assert
            using (new AssertionScope())
            {
                response.StatusCode.Should().Be(HttpStatusCode.OK);

                var result = await response.ReadAsync<PingMonitoringEntityResultModel>();
                result.Address.Should().Be(MonitoringEntityAddress);
                result.PeriodInMinutes.Should().Be(PeriodInMinutes);

                var monitoringEntity = await dbContext.MonitoringEntities.FirstOrDefaultAsync(i => i.Id == result.Id);
                monitoringEntity.Address.Should().Be(MonitoringEntityAddress);
                monitoringEntity.PeriodInMinutes.Should().Be(PeriodInMinutes);
            }
        }

        private static Site CreateSiteWithMonitoringEntity()
        {
            return new Site
            {
                MonitoringEntities = new[]
                {
                    new MonitoringEntity
                    {
                        Type = MonitoringType.Ping,
                        Address = MonitoringEntityAddress,
                        PeriodInMinutes = PeriodInMinutes
                    }
                }
            };
        }
    }
}