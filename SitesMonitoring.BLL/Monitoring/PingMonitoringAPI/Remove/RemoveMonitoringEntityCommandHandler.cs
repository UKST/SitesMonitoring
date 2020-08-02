using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SitesMonitoring.BLL.ErrorHandling;

namespace SitesMonitoring.BLL.Monitoring.PingMonitoringAPI.Remove
{
    public class RemoveMonitoringEntityCommandHandler : IRequestHandler<RemoveMonitoringEntityCommand>
    {
        private readonly IMonitoringEntityRepository _monitoringEntityRepository;
        private readonly IMonitoringValidator _validator;

        public RemoveMonitoringEntityCommandHandler(
            IMonitoringEntityRepository monitoringEntityRepository,
            IMonitoringValidator validator)
        {
            _monitoringEntityRepository = monitoringEntityRepository;
            _validator = validator;
        }

        public async Task<Unit> Handle(RemoveMonitoringEntityCommand request, CancellationToken cancellationToken)
        {
            await _validator.ValidateSiteExistenceAsync(request.SiteId);

            var entity = await _monitoringEntityRepository.GetByIdAsync(request.Id);
            if (entity == null) throw new NotFoundException();

            await _monitoringEntityRepository.RemoveAsync(entity);

            return new Unit();
        }
    }
}