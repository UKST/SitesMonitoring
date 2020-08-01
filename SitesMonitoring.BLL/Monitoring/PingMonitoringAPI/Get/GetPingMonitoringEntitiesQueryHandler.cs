using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace SitesMonitoring.BLL.Monitoring.PingMonitoringAPI.Get
{
    public class GetPingMonitoringEntitiesQueryHandler : IRequestHandler<GetPingMonitoringEntitiesQuery, ICollection<MonitoringEntity>>
    {
        private readonly IMonitoringEntityRepository _monitoringEntityRepository;
        private readonly IMonitoringValidator _validator;

        public GetPingMonitoringEntitiesQueryHandler(
            IMonitoringEntityRepository monitoringEntityRepository,
            IMonitoringValidator validator)
        {
            _monitoringEntityRepository = monitoringEntityRepository;
            _validator = validator;
        }

        public Task<ICollection<MonitoringEntity>> Handle(GetPingMonitoringEntitiesQuery request, CancellationToken cancellationToken)
        {
            _validator.ValidateSiteExistence(request.SiteId);

            return Task.FromResult<ICollection<MonitoringEntity>>(_monitoringEntityRepository.GetBySiteId(request.SiteId).ToArray());
        }
    }
}