using System;
using System.Collections.Generic;
using SitesMonitoring.BLL.Data;

namespace SitesMonitoring.BLL.Monitoring
{
    public interface IMonitoringEntityRepository : IRepository<MonitoringEntity>
    {
        IEnumerable<MonitoringEntity> GetBySiteId(int siteId);
        IEnumerable<MonitoringEntity> GetByMonitoringPeriods(IEnumerable<TimeSpan> periods);
    }
}