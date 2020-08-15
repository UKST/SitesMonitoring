using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SitesMonitoring.BLL.Data;

namespace SitesMonitoring.BLL.Monitoring.PingMonitoringAPI.Create
{
    public class CreatePingMonitoringEntityCommandHandler : IRequestHandler<CreatePingMonitoringEntityCommand, MonitoringEntity>
    {
        private readonly IMonitoringValidator _validator;
        private readonly IRepository<MonitoringEntity> _monitoringEntityRepository;

        public CreatePingMonitoringEntityCommandHandler(
            IMonitoringValidator validator,
            IRepository<MonitoringEntity> monitoringEntityRepository)
        {
            _validator = validator;
            _monitoringEntityRepository = monitoringEntityRepository;
        }

        public async Task<MonitoringEntity> Handle(CreatePingMonitoringEntityCommand request, CancellationToken cancellationToken)
        {
             await _validator.ValidateEntityAsync(request.Entity);
             _monitoringEntityRepository.Create(request.Entity);
             await _monitoringEntityRepository.SaveChangesAsync();

            return request.Entity;
        }
    }
}