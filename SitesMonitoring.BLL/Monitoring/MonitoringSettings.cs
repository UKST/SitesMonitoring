using System.Collections.Generic;
using System.Linq;

namespace SitesMonitoring.BLL.Monitoring
{
    public sealed class MonitoringSettings : IMonitoringSettings
    {
        // todo test these intervals (should be in 1 <= n < 60 (<= 24), should be at least 1 value), n % (24 / 60) == 0
        public ICollection<int> AvailableMonitoringPeriodsInMinutes { get; } = new List<int> {1, 5, 10, 15, 30};

        public ICollection<int> AvailableMonitoringPeriodsInHours { get; } = new List<int> {1, 2, 3, 6, 12, 24};

        public ICollection<int> CombinedMonitoringPeriodsInMinutes => AvailableMonitoringPeriodsInMinutes
            .Union(AvailableMonitoringPeriodsInHours
                .Select(i => i * MonitoringConstants.MinutesInHour)).ToArray();
    }
}