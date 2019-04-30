using SitesMonitoring.BLL.Monitoring;

namespace SitesMonitoring.DAL
{
    public class MonitoringResultInMemoryRepository : InMemoryRepository<MonitoringResult>, IMonitoringResultRepository
    {
        public MonitoringResult GetLast(long monitoringEntityId)
        {
            return Entities.FindLast(i => i.MonitoringEntityId == monitoringEntityId);
        }
    }
}