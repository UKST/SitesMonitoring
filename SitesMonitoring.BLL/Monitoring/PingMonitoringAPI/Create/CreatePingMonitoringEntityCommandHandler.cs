using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using SitesMonitoring.BLL.Data;

namespace SitesMonitoring.BLL.Monitoring.PingMonitoringAPI.Create
{
    public class CreatePingMonitoringEntityCommandHandler : IRequestHandler<CreatePingMonitoringEntityCommand, MonitoringEntity>
    {
        private readonly IRepository<MonitoringEntity> _monitoringEntityRepository;

        public CreatePingMonitoringEntityCommandHandler(
            IRepository<MonitoringEntity> monitoringEntityRepository)
        {
            _monitoringEntityRepository = monitoringEntityRepository;
        }

        public async Task<MonitoringEntity> Handle(CreatePingMonitoringEntityCommand request, CancellationToken cancellationToken)
        {
            _monitoringEntityRepository.Create(request.Entity);
            await _monitoringEntityRepository.SaveChangesAsync();

            return request.Entity;
        }
    }
}