namespace SitesMonitoring.BLL.Monitoring
{
    public interface IMonitoringRequest<TResult>
    {
        TResult Send(MonitoringEntity entity);
    }
}