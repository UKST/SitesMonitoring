using System.Collections.Generic;
using System.Linq;
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
        
        public SiteHealth GetHealth(Site site)
        {
            var monitoringEntities = _monitoringEntityRepository.GetBySiteId(site.Id).Where(i => i.Type == MonitoringType.Ping);
            var siteHealthResults = monitoringEntities
                .Select(monitoringEntity => _monitoringResultRepository.GetLast(monitoringEntity.Id))
                .Select(monitoringResult => _healthStatusMapper.Map(monitoringResult.Data))
                .ToList();

            if (siteHealthResults.Any(i => i == SiteHealth.Good) && siteHealthResults.Any(i => i == SiteHealth.Bad))
                return SiteHealth.Normal;

            return siteHealthResults.Any() ? siteHealthResults.First() : SiteHealth.Unknown;
        }
    }
}