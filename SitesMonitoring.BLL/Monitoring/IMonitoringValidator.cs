using System.Threading.Tasks;

namespace SitesMonitoring.BLL.Monitoring
{
    public interface IMonitoringValidator
    {
        Task ValidateEntityAsync(MonitoringEntity entity);
        Task ValidateSiteExistenceAsync(long siteId);
    }
}