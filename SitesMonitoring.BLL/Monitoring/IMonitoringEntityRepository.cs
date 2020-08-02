using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SitesMonitoring.BLL.Data;

namespace SitesMonitoring.BLL.Monitoring
{
    public interface IMonitoringEntityRepository : IRepository<MonitoringEntity>
    {
        Task<IEnumerable<MonitoringEntity>> GetBySiteIdAsync(long siteId);
        Task<IEnumerable<MonitoringEntity>> GetByMonitoringPeriodsAsync(IEnumerable<int> periods);
    }
}