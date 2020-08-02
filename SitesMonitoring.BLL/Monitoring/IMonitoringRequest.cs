using System.Threading.Tasks;

namespace SitesMonitoring.BLL.Monitoring
{
    public interface IMonitoringRequest<TResult>
    {
        Task<TResult> SendAsync(MonitoringEntity entity);
    }
}