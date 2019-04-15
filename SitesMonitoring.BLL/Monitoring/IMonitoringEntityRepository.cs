using System;
using System.Collections.Generic;
using SitesMonitoring.BLL.Data;

namespace SitesMonitoring.BLL.Monitoring
{
    public interface IMonitoringEntityRepository : IRepository<MonitoringEntity>
    {
        IEnumerable<MonitoringEntity> GetBySiteId(long siteId);
        IEnumerable<MonitoringEntity> GetByMonitoringPeriods(IEnumerable<int> periods);
    }
}