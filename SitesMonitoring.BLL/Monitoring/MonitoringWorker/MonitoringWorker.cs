using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using SitesMonitoring.BLL.Utils;

namespace SitesMonitoring.BLL.Monitoring.MonitoringWorker
{
    public class MonitoringWorker : IMonitoringWorker, IDisposable
    {
        private const int MinutesInHour = 60;
        private const int HoursInDay = 24;
        
        private readonly IEnumerable<Lazy<IMonitoringProcess, MonitoringProcessMetadata>> _monitoringProcesses;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly IMonitoringEntityRepository _monitoringEntityRepository;
        
        private Timer _timer;

        public MonitoringWorker(
            IEnumerable<Lazy<IMonitoringProcess, MonitoringProcessMetadata>> monitoringProcesses,
            IDateTimeProvider dateTimeProvider,
            IMonitoringEntityRepository monitoringEntityRepository)
        {
            _dateTimeProvider = dateTimeProvider;
            _monitoringProcesses = monitoringProcesses;
            _monitoringEntityRepository = monitoringEntityRepository;
        }
        
        public void Start()
        {
            // todo handle finalization / cancellation
            var now = _dateTimeProvider.Now;

            var minMonitoringPeriod = MonitoringConstants.AvailableMonitoringIntervalsInMinutes.Min();
            var dueTime = GetDueTime(now);
            
            _timer = new Timer(
                RunMonitoringProcesses,
                null,
                dueTime,
                TimeSpan.FromMinutes(minMonitoringPeriod));
            
            Console.WriteLine("MonitoringHandler started");
        }

        private static TimeSpan GetDueTime(DateTime now)
        {
            var minutesWhenMonitoringShouldStart = GetMinutesWhenMonitoringShouldStart().ToArray();
            var diffsWithCurrentMinute = minutesWhenMonitoringShouldStart.Select(i => i - now.Minute).ToArray();
            var minuteOfStart =
                minutesWhenMonitoringShouldStart[
                    Array.IndexOf(diffsWithCurrentMinute, diffsWithCurrentMinute.Where(i => i > 0).Min())];
            
            var dueTime = TimeSpan.FromMinutes(minuteOfStart - now.Minute);
            
            return dueTime.Add(-TimeSpan.FromSeconds(now.Second));
        }

        private static IEnumerable<int> GetMinutesWhenMonitoringShouldStart()
        {
            var allPossibleMinutes = new List<int>();
            foreach (var interval in MonitoringConstants.AvailableMonitoringIntervalsInMinutes)
            {
                allPossibleMinutes.AddRange(TimeRange(0, MinutesInHour, interval));
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

        private void RunMonitoringProcesses(object state)
        {
            var periods = GetPeriodsOfMonitoring();
            var monitorEntries = _monitoringEntityRepository.GetByMonitoringPeriods(periods);
            var sitesMonitors = monitorEntries.GroupBy(i => i.SiteId);

            foreach (var siteMonitors in sitesMonitors)
            {
                foreach (var monitor in siteMonitors)
                {
                    var process = _monitoringProcesses.SingleOrDefault(a => a.Metadata.Type == monitor.Type);

                    if (process == null)
                        throw new NotImplementedException(
                            $"There is no implementation of {nameof(IMonitoringProcess)} for {nameof(MonitoringType)}: {monitor.Type}");
                    
                    process.Value.Start(monitor);
                }
            }
        }

        private IEnumerable<int> GetPeriodsOfMonitoring()
        {
            var now = _dateTimeProvider.Now;

            var minutesIntervals = MonitoringConstants.AvailableMonitoringIntervalsInMinutes.Where(i =>
                i % now.Minute == 0);

            if (now.Minute != 0) return minutesIntervals;

            var hoursIntervals =
                MonitoringConstants.AvailableMonitoringIntervalsInHours.Where(i => i % now.Hour == 0);
            return minutesIntervals.Union(hoursIntervals.Select(i => i * MinutesInHour));
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}