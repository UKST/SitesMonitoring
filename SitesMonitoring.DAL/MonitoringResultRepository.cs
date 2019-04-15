using SitesMonitoring.BLL.Monitoring;

namespace SitesMonitoring.DAL
{
    public class MonitoringResultRepository : Repository<MonitoringResult>, IMonitoringResultRepository
    {
        public MonitoringResult GetLast(long monitoringEntityId)
        {
            return Entities.FindLast(i => i.MonitoringEntityId == monitoringEntityId);
        }
    }
}