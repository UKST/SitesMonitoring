using System.Collections.Generic;
using System.Linq;
using SitesMonitoring.BLL.ErrorHandling;

namespace SitesMonitoring.BLL.Monitoring.PingMonitoringAPI
{
    public sealed class PingMonitoringService : IMonitoringService
    {
        private readonly IMonitoringEntityRepository _monitoringEntityRepository;
        private readonly IMonitoringValidator _validator;
        
        public PingMonitoringService(
            IMonitoringEntityRepository monitoringEntityRepository,
            IMonitoringValidator validator)
        {
            _monitoringEntityRepository = monitoringEntityRepository;
            _validator = validator;
        }
        
        public ICollection<MonitoringEntity> GetAllEntities(int siteId)
        {
            _validator.ValidateSiteExistence(siteId);
            
            return _monitoringEntityRepository.GetBySiteId(siteId).ToArray();
        }

        public MonitoringEntity CreateEntity(MonitoringEntity entity)
        {
            _validator.ValidateEntity(entity);
            _monitoringEntityRepository.Create(entity);

            return entity;
        }

        public void RemoveEntity(int siteId, int id)
        {
            _validator.ValidateSiteExistence(siteId);
            
            var entity = _monitoringEntityRepository.GetById(id);
            if (entity == null) throw new NotFoundException();
            
            _monitoringEntityRepository.Remove(entity);
        }
    }
}