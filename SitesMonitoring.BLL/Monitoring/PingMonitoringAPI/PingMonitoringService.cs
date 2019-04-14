using System;
using System.Collections.Generic;
using System.Linq;

namespace SitesMonitoring.BLL.Monitoring.PingMonitoringAPI
{
    public sealed class PingMonitoringService : IMonitoringService
    {
        private readonly IMonitoringEntityRepository _monitoringEntityRepository;
        private readonly IMonitoringEntityValidator _validator;
        
        public PingMonitoringService(
            IMonitoringEntityRepository monitoringEntityRepository,
            IMonitoringEntityValidator validator)
        {
            _monitoringEntityRepository = monitoringEntityRepository;
            _validator = validator;
        }
        
        public ICollection<MonitoringEntity> GetAllEntities(int siteId)
        {
            // todo validate if user have access to siteId
            return _monitoringEntityRepository.GetBySiteId(siteId).ToArray();
        }

        public MonitoringEntity CreateEntity(MonitoringEntity entity)
        {
            // todo validate if user have access to siteId
            _validator.Validate(entity);
            _monitoringEntityRepository.Create(entity);

            return entity;
        }

        public void RemoveEntity(int siteId, int id)
        {
            // todo validate if user have access to siteId
            var entity = _monitoringEntityRepository.GetById(id);
            if (entity == null) throw new ArgumentException();
            _monitoringEntityRepository.Remove(entity);
        }
    }
}