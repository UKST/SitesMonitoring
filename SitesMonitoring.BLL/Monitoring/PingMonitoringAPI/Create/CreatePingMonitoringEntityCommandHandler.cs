using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace SitesMonitoring.BLL.Monitoring.PingMonitoringAPI
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

        public Task<MonitoringEntity> Handle(CreatePingMonitoringEntityCommand request, CancellationToken cancellationToken)
        {
            _validator.ValidateEntity(request.Entity);
            _monitoringEntityRepository.Create(request.Entity);

            return Task.FromResult(request.Entity);
        }
    }
}