using System.Threading.Tasks;
using SitesMonitoring.BLL.Data;

namespace SitesMonitoring.BLL.Monitoring
{
    public interface IMonitoringResultRepository : IRepository<MonitoringResult>
    {
        Task<MonitoringResult> GetLastAsync(long monitoringEntityId);
    }
}