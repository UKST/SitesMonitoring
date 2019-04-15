using SitesMonitoring.BLL.Data;

namespace SitesMonitoring.BLL.Monitoring
{
    public interface IMonitoringResultRepository : IRepository<MonitoringResult>
    {
        MonitoringResult GetLast(long monitoringEntityId);
    }
}