using System.Collections.Generic;
using System.Linq;
using SitesMonitoring.BLL.Monitoring;

namespace SitesMonitoring.DAL
{
    public class MonitoringEntityRepository : IMonitoringRepository<MonitoringEntity>
    {
        private readonly List<MonitoringEntity> _monitoringEntities = new List<MonitoringEntity>();
        
        public MonitoringEntity GetById(int id)
        {
            return _monitoringEntities.Single(i => i.Id == id);
        }

        public IEnumerable<MonitoringEntity> GetAll()
        {
            return _monitoringEntities;
        }

        public void Create(MonitoringEntity item)
        {
            // todo - increment id
            _monitoringEntities.Add(item);
        }

        public void Remove(MonitoringEntity item)
        {
            _monitoringEntities.Remove(item);
        }

        public IEnumerable<MonitoringEntity> GetBySiteId(int siteId)
        {
            return _monitoringEntities.Where(i => i.SiteId == siteId);
        }
    }
}