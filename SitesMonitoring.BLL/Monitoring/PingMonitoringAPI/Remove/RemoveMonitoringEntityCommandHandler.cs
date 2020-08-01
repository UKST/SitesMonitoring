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

        public Task<Unit> Handle(RemoveMonitoringEntityCommand request, CancellationToken cancellationToken)
        {
            _validator.ValidateSiteExistence(request.SiteId);

            var entity = _monitoringEntityRepository.GetById(request.Id);
            if (entity == null) throw new NotFoundException();

            _monitoringEntityRepository.Remove(entity);

            return Task.FromResult(new Unit());
        }
    }
}