using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SitesMonitoring.BLL.Monitoring.MonitoringWorker;

namespace SitesMonitoring.API
{
    public class MonitoringHostedService : IHostedService
    {
        private readonly ILogger _logger;
        private readonly IMonitoringWorker _monitoringWorker;

        public MonitoringHostedService(
            ILogger<MonitoringHostedService> logger,
            IMonitoringWorker monitoringWorker)
        {
            _logger = logger;
            _monitoringWorker = monitoringWorker;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Timed Background Service is starting.");
            
            _monitoringWorker.Start();

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Timed Background Service is stopping.");

            _monitoringWorker.Stop();

            return Task.CompletedTask;
        }
    }
}