using System.Threading.Tasks;

namespace SitesMonitoring.BLL.Monitoring.MonitoringWorker
{
    public interface IMonitoringProcess
    {
        Task StartAsync(MonitoringEntity entity);
    }
}