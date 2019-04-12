using System.Collections.Generic;

namespace SitesMonitoring.BLL.Monitoring
{
    public interface IMonitoringService
    {
        ICollection<MonitoringEntity> GetAllEntities(int siteId);
        MonitoringEntity CreateEntity(MonitoringEntity entity);
        void RemoveEntity(int siteId, int id);
    }
}