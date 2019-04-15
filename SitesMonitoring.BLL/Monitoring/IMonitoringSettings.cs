using System.Collections.Generic;

namespace SitesMonitoring.BLL.Monitoring
{
    public interface IMonitoringSettings
    {
        ICollection<int> AvailableMonitoringPeriodsInMinutes { get; }
        ICollection<int> AvailableMonitoringPeriodsInHours { get; }
        ICollection<int> CombinedMonitoringPeriodsInMinutes { get; }
    }
}