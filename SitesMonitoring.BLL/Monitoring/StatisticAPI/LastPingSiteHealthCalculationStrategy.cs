using System.Linq;
using System.Threading.Tasks;
using MediatR;
using SitesMonitoring.BLL.Monitoring.SitesAPI;

namespace SitesMonitoring.BLL.Monitoring.StatisticAPI
{
    public class LastPingSiteHealthCalculationStrategy : ISiteHealthCalculationStrategy
    {
        private readonly IMediator _mediator;
        private readonly IHealthStatusMapper _healthStatusMapper;

        public LastPingSiteHealthCalculationStrategy(
            IMediator mediator,
            IHealthStatusMapper healthStatusMapper)
        {
            _mediator = mediator;
            _healthStatusMapper = healthStatusMapper;
        }

        public async Task<SiteHealth> GetHealthAsync(Site site)
        {
            var monitoringResults = await _mediator.Send(new GetMonitoringEntitiesLastResultsQuery(site.Id));
            var siteHealthResults = monitoringResults.Select(_healthStatusMapper.Map).ToArray();

            if (siteHealthResults.Any(i => i == SiteHealth.Good) && siteHealthResults.Any(i => i == SiteHealth.Bad))
                return SiteHealth.Normal;

            return siteHealthResults.Any() ? siteHealthResults.First() : SiteHealth.Unknown;
        }
    }
}