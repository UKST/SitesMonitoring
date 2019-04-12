using System.Collections.Generic;
using System.Linq;

namespace SitesMonitoring.BLL.Monitoring.Ping
{
    public sealed class PingMonitoringService : IMonitoringService
    {
        private readonly IMonitoringRepository<MonitoringEntity> _repository;
        private readonly IMonitoringEntityValidator _validator;
        
        public PingMonitoringService(
            IMonitoringRepository<MonitoringEntity> repository,
            IMonitoringEntityValidator validator)
        {
            _repository = repository;
            _validator = validator;
        }
        
        public ICollection<MonitoringEntity> GetAllEntities(int siteId)
        {
            // todo validate if user have access to siteId
            return _repository.GetBySiteId(siteId).ToArray();
        }

        public MonitoringEntity CreateEntity(MonitoringEntity entity)
        {
            // todo validate if user have access to siteId
            _validator.Validate(entity);
            _repository.Create(entity);

            return entity;
        }

        public void RemoveEntity(int siteId, int id)
        {
            // todo validate if user have access to siteId
            var entity = _repository.GetById(id);
            _repository.Remove(entity);
        }
    }
}