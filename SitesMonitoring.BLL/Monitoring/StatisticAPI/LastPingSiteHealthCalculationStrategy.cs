using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SitesMonitoring.BLL.Monitoring.SitesAPI;

namespace SitesMonitoring.BLL.Monitoring.StatisticAPI
{
    public class LastPingSiteHealthCalculationStrategy : ISiteHealthCalculationStrategy
    {
        private readonly IMonitoringResultRepository _monitoringResultRepository;
        private readonly IMonitoringEntityRepository _monitoringEntityRepository;
        private readonly IHealthStatusMapper _healthStatusMapper;

        public LastPingSiteHealthCalculationStrategy(
            IMonitoringResultRepository monitoringResultRepository,
            IMonitoringEntityRepository monitoringEntityRepository,
            IHealthStatusMapper healthStatusMapper)
        {
            _monitoringResultRepository = monitoringResultRepository;
            _monitoringEntityRepository = monitoringEntityRepository;
            _healthStatusMapper = healthStatusMapper;
        }

        public async Task<SiteHealth> GetHealthAsync(Site site)
        {
            // todo optimize query - separate repository / strategy to retrieve data for current method
            var monitoringEntities = (await _monitoringEntityRepository.GetBySiteIdAsync(site.Id))
                .Where(i => i.Type == MonitoringType.Ping)
                .ToArray();
            var siteHealthResults = new List<SiteHealth>();

            foreach (var monitoringEntity in monitoringEntities)
            {
                var monitoringResult = await _monitoringResultRepository.GetLastAsync(monitoringEntity.Id);

                if (monitoringResult != null)
                {
                    siteHealthResults.Add(_healthStatusMapper.Map(monitoringResult));
                }
            }

            if (siteHealthResults.Any(i => i == SiteHealth.Good) && siteHealthResults.Any(i => i == SiteHealth.Bad))
                return SiteHealth.Normal;

            return siteHealthResults.Any() ? siteHealthResults.First() : SiteHealth.Unknown;
        }
    }
}