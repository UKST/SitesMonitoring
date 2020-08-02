using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SitesMonitoring.BLL.Monitoring;

namespace SitesMonitoring.DAL
{
    public class MonitoringEntityRepository : Repository<MonitoringEntity>, IMonitoringEntityRepository
    {
        public MonitoringEntityRepository(SitesMonitoringDbContext db) : base(db)
        {
        }

        public async Task<IEnumerable<MonitoringEntity>> GetBySiteIdAsync(long siteId)
        {
            return await Db.MonitoringEntities.Where(i => i.SiteId == siteId).ToArrayAsync();
        }

        public async Task<IEnumerable<MonitoringEntity>> GetByMonitoringPeriodsAsync(IEnumerable<int> periods)
        {
            return await Db.MonitoringEntities.Where(i => periods.Contains(i.PeriodInMinutes)).ToArrayAsync();
        }
    }
}