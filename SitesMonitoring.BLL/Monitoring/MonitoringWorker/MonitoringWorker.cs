using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using SitesMonitoring.BLL.Utils;

namespace SitesMonitoring.BLL.Monitoring.MonitoringWorker
{
    public class MonitoringWorker : IMonitoringWorker, IDisposable
    {
        private readonly IEnumerable<Lazy<IMonitoringProcess, MonitoringProcessMetadata>> _monitoringProcesses;
        private readonly IMonitoringEntityRepository _monitoringEntityRepository;
        private readonly IMonitoringSettings _monitoringSettings;
        private readonly IMonitoringPeriodsProvider _monitoringPeriodsProvider;
        
        private Timer _timer;

        public MonitoringWorker(
            IEnumerable<Lazy<IMonitoringProcess, MonitoringProcessMetadata>> monitoringProcesses,
            IMonitoringEntityRepository monitoringEntityRepository,
            IMonitoringSettings monitoringSettings,
            IMonitoringPeriodsProvider monitoringPeriodsProvider)
        {
            _monitoringProcesses = monitoringProcesses;
            _monitoringEntityRepository = monitoringEntityRepository;
            _monitoringSettings = monitoringSettings;
            _monitoringPeriodsProvider = monitoringPeriodsProvider;
        }
        
        public void Start()
        {
            var minMonitoringPeriod = _monitoringSettings.AvailableMonitoringPeriodsInMinutes.Min();
            var dueTime = _monitoringPeriodsProvider.GetMonitoringStartDueTime();
            
            _timer = new Timer(
                RunMonitoringProcesses,
                null,
                dueTime,
                TimeSpan.FromMinutes(minMonitoringPeriod));
        }

        public void Stop()
        {
            _timer.Change(Timeout.Infinite, 0);
        }

        private void RunMonitoringProcesses(object state)
        {
            var periods = _monitoringPeriodsProvider.GetPeriodsOfMonitoringInMinutes();
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

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}