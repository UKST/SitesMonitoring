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

        public async Task<ICollection<MonitoringEntity>> Handle(GetPingMonitoringEntitiesQuery request, CancellationToken cancellationToken)
        {
            await _validator.ValidateSiteExistenceAsync(request.SiteId);

            return (await _monitoringEntityRepository.GetBySiteIdAsync(request.SiteId)).ToArray();
        }
    }
}