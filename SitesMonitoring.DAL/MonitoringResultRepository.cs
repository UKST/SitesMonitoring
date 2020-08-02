using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SitesMonitoring.BLL.Monitoring;

namespace SitesMonitoring.DAL
{
    public class MonitoringResultRepository : Repository<MonitoringResult>, IMonitoringResultRepository
    {
        public MonitoringResultRepository(SitesMonitoringDbContext db) : base(db)
        {
        }

        public async Task<MonitoringResult> GetLastAsync(long monitoringEntityId)
        {
            return await Db.MonitoringResults
                .OrderByDescending(i => i.Id)
                .FirstOrDefaultAsync(i => i.MonitoringEntityId == monitoringEntityId);
        }
    }
}