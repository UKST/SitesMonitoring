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
        private readonly IMonitoringValidator _validator;

        public RemoveMonitoringEntityCommandHandler(
            IRepository<MonitoringEntity> monitoringEntityRepository,
            IMonitoringValidator validator)
        {
            _monitoringEntityRepository = monitoringEntityRepository;
            _validator = validator;
        }

        public async Task<Unit> Handle(RemoveMonitoringEntityCommand request, CancellationToken cancellationToken)
        {
            await _validator.ValidateSiteExistenceAsync(request.SiteId);

            var entity = await _monitoringEntityRepository.GetFirstOrDefaultAsync(new GetByIdSpecification<MonitoringEntity>(request.Id));
            if (entity == null) throw new NotFoundException();

            _monitoringEntityRepository.Remove(entity);
            await _monitoringEntityRepository.SaveChangesAsync();

            return new Unit();
        }
    }
}