using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace SitesMonitoring.BLL.Monitoring.PingMonitoringAPI.Create
{
    public class CreatePingMonitoringEntityCommandHandler : IRequestHandler<CreatePingMonitoringEntityCommand, MonitoringEntity>
    {
        private readonly IMonitoringValidator _validator;
        private readonly IMonitoringEntityRepository _monitoringEntityRepository;

        public CreatePingMonitoringEntityCommandHandler(
            IMonitoringValidator validator,
            IMonitoringEntityRepository monitoringEntityRepository)
        {
            _validator = validator;
            _monitoringEntityRepository = monitoringEntityRepository;
        }

        public async Task<MonitoringEntity> Handle(CreatePingMonitoringEntityCommand request, CancellationToken cancellationToken)
        {
             await _validator.ValidateEntityAsync(request.Entity);
             await _monitoringEntityRepository.CreateAsync(request.Entity);

            return request.Entity;
        }
    }
}