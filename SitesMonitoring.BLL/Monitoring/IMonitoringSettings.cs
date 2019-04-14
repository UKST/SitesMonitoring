using System.Collections.Generic;

namespace SitesMonitoring.BLL.Monitoring
{
    public interface IMonitoringSettings
    {
        ICollection<int> AvailableMonitoringIntervalsInMinutes { get; }
        ICollection<int> AvailableMonitoringIntervalsInHours { get; }
    }
}