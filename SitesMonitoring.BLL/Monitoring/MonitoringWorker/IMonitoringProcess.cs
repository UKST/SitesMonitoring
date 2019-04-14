namespace SitesMonitoring.BLL.Monitoring.MonitoringWorker
{
    public interface IMonitoringProcess
    {
        void Start(MonitoringEntity entity);
    }
}