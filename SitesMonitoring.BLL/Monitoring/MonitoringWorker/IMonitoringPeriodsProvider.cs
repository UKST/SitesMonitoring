using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SitesMonitoring.BLL.Monitoring.MonitoringWorker
{
    public interface IMonitoringPeriodsProvider
    {
        TimeSpan GetMonitoringStartDueTime();
        IEnumerable<int> GetPeriodsOfMonitoringInMinutes();
    }
}