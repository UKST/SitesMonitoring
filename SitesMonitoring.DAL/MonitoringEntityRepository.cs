using System.Collections.Generic;
using System.Linq;
using SitesMonitoring.BLL.Monitoring;

namespace SitesMonitoring.DAL
{
    public class MonitoringEntityRepository : Repository<MonitoringEntity>, IMonitoringEntityRepository
    {
        public MonitoringEntityRepository(SitesMonitoringDbContext db) : base(db)
        {
        }

        public IEnumerable<MonitoringEntity> GetBySiteId(long siteId)
        {
            return Db.MonitoringEntities.Where(i => i.SiteId == siteId);
        }

        public IEnumerable<MonitoringEntity> GetByMonitoringPeriods(IEnumerable<int> periods)
        {
            return Db.MonitoringEntities.Where(i => periods.Contains(i.PeriodInMinutes));
        }
    }
}