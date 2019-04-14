using System;

namespace SitesMonitoring.BLL.Monitoring
{
    public static class MonitoringConstants
    {
        // todo test these intervals (should be in 1 <= n < 60 (<= 24), should be at least 1 value)
        public static readonly int[] AvailableMonitoringIntervalsInMinutes = {1, 5, 10, 15, 30};
        public static readonly int[] AvailableMonitoringIntervalsInHours = {1, 2, 3, 6, 12, 24};
    }
}