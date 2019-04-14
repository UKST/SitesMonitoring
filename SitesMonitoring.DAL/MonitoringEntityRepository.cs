using System.Collections.Generic;
using System.Linq;
using SitesMonitoring.BLL.Monitoring;

namespace SitesMonitoring.DAL
{
    public class MonitoringEntityRepository : Repository<MonitoringEntity>, IMonitoringEntityRepository
    {
        public IEnumerable<MonitoringEntity> GetBySiteId(int siteId)
        {
            return Entities.Where(i => i.SiteId == siteId);
        }

        public IEnumerable<MonitoringEntity> GetByMonitoringPeriods(IEnumerable<int> periods)
        {
            return Entities.Where(i => periods.Contains(i.PeriodInMinutes));
        }
    }
}