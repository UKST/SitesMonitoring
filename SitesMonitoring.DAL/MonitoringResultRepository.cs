using System.Linq;
using SitesMonitoring.BLL.Monitoring;

namespace SitesMonitoring.DAL
{
    public class MonitoringResultRepository : Repository<MonitoringResult>, IMonitoringResultRepository
    {
        public MonitoringResultRepository(SitesMonitoringDbContext db) : base(db)
        {
        }

        public MonitoringResult GetLast(long monitoringEntityId)
        {
            return Db.MonitoringResults.LastOrDefault(i => i.MonitoringEntityId == monitoringEntityId);
        }
    }
}