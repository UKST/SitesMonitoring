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
            var minutesWhenMonitoringShouldStart = MonitoringConstants.AvailableMonitoringIntervalsInMinutes
                .Select(i => i % MinutesInHour).Distinct().ToArray();
            var diffsWithCurrentMinute = minutesWhenMonitoringShouldStart.Select(i => i - now.Minute).ToArray();
            var minuteOfStart =
                minutesWhenMonitoringShouldStart[
                    Array.IndexOf(diffsWithCurrentMinute, diffsWithCurrentMinute.Where(i => i > 0).Min())];
            
            var dueTime = TimeSpan.FromMinutes(minuteOfStart - now.Minute);
            
            return dueTime.Add(-TimeSpan.FromSeconds(now.Second));
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

        private IEnumerable<TimeSpan> GetPeriodsOfMonitoring()
        {
            var now = _dateTimeProvider.Now;

            return MonitoringConstants.AvailableMonitoringIntervalsInMinutes.Where(i =>
                i % MinutesInHour == now.Minute).Select(i => TimeSpan.FromMinutes(i));
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}