using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SitesMonitoring.BLL.Data;
using SitesMonitoring.BLL.ErrorHandling;

namespace SitesMonitoring.BLL.Monitoring.PingMonitoringAPI.Remove
{
    public class RemoveMonitoringEntityCommandHandler : IRequestHandler<RemoveMonitoringEntityCommand>
    {
        private readonly IRepository<MonitoringEntity> _monitoringEntityRepository;

        public RemoveMonitoringEntityCommandHandler(
            IRepository<MonitoringEntity> monitoringEntityRepository)
        {
            _monitoringEntityRepository = monitoringEntityRepository;
        }

        public async Task<Unit> Handle(RemoveMonitoringEntityCommand request, CancellationToken cancellationToken)
        {
            var entity = await _monitoringEntityRepository.GetFirstOrDefaultAsync(new GetByIdSpecification<MonitoringEntity>(request.Id));
            _monitoringEntityRepository.Remove(entity);
            await _monitoringEntityRepository.SaveChangesAsync();

            return new Unit();
        }
    }
}