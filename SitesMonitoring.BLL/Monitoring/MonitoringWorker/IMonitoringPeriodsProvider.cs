using System;
using System.Collections.Generic;

namespace SitesMonitoring.BLL.Monitoring.MonitoringWorker
{
    public interface IMonitoringPeriodsProvider
    {
        TimeSpan GetMonitoringStartDueTime();
        IEnumerable<int> GetPeriodsOfMonitoringInMinutes();
    }
}