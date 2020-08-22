using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SitesMonitoring.BLL.Data;

namespace SitesMonitoring.BLL.Monitoring.PingMonitoringAPI.Get
{
    public class GetPingMonitoringEntitiesQueryHandler : IRequestHandler<GetPingMonitoringEntitiesQuery, ICollection<MonitoringEntity>>
    {
        private readonly IRepository<MonitoringEntity> _monitoringEntityRepository;

        public GetPingMonitoringEntitiesQueryHandler(
            IRepository<MonitoringEntity> monitoringEntityRepository)
        {
            _monitoringEntityRepository = monitoringEntityRepository;
        }

        public async Task<ICollection<MonitoringEntity>> Handle(GetPingMonitoringEntitiesQuery request, CancellationToken cancellationToken)
        {
            return (await _monitoringEntityRepository.GetManyAsync(new GetByIdSpecification<MonitoringEntity>(request.SiteId))).ToArray();
        }
    }
}