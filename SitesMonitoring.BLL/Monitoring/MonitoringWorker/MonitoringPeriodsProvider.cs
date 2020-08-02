using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SitesMonitoring.BLL.Utils;

namespace SitesMonitoring.BLL.Monitoring.MonitoringWorker
{
    public class MonitoringPeriodsProvider : IMonitoringPeriodsProvider
    {
        private readonly IMonitoringSettings _monitoringSettings;
        private readonly IDateTimeProvider _dateTimeProvider;

        public MonitoringPeriodsProvider(
            IMonitoringSettings monitoringSettings,
            IDateTimeProvider dateTimeProvider)
        {
            _monitoringSettings = monitoringSettings;
            _dateTimeProvider = dateTimeProvider;
        }

        public TimeSpan GetMonitoringStartDueTime()
        {
            var now = _dateTimeProvider.Now;

            var minutesWhenMonitoringShouldStart = GetMinutesWhenMonitoringShouldStart().ToArray();

            if (!minutesWhenMonitoringShouldStart.Any())
                return TimeSpan.MaxValue;

            var diffsWithCurrentMinute = minutesWhenMonitoringShouldStart.Select(i => i - now.Minute).ToArray();
            var minuteOfStart =
                minutesWhenMonitoringShouldStart[
                    Array.IndexOf(diffsWithCurrentMinute, diffsWithCurrentMinute.Where(i => i > 0).Min())];

            var dueTime = TimeSpan.FromMinutes(minuteOfStart - now.Minute);

            return dueTime.Add(-TimeSpan.FromSeconds(now.Second));
        }

        public IEnumerable<int> GetPeriodsOfMonitoringInMinutes()
        {
            var now = _dateTimeProvider.Now;

            var minutesIntervals = _monitoringSettings.AvailableMonitoringPeriodsInMinutes
                .Where(i => now.Minute == 0 || now.Minute % i == 0);

            if (now.Minute != 0) return minutesIntervals;

            var hoursIntervals = _monitoringSettings.AvailableMonitoringPeriodsInHours
                .Where(i => now.Hour == 0 || now.Hour % i == 0);
            return minutesIntervals.Union(hoursIntervals.Select(i => i * MonitoringConstants.MinutesInHour));
        }

        private IEnumerable<int> GetMinutesWhenMonitoringShouldStart()
        {
            var allPossibleMinutes = new List<int>();
            foreach (var interval in _monitoringSettings.AvailableMonitoringPeriodsInMinutes)
            {
                allPossibleMinutes.AddRange(TimeRange(0, MonitoringConstants.MinutesInHour, interval));
            }

            return allPossibleMinutes.Distinct();
        }

        private static IEnumerable<int> TimeRange(int from, int to, int step)
        {
            for (var item = from; item <= to; item+=step)
            {
                yield return item;
            }
        }
    }
}