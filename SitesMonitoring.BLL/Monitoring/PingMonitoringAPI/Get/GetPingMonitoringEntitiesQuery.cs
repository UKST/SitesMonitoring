using System.Collections.Generic;
using MediatR;

namespace SitesMonitoring.BLL.Monitoring.PingMonitoringAPI.Get
{
    public class GetPingMonitoringEntitiesQuery : IRequest<ICollection<MonitoringEntity>>
    {
        public long SiteId { get; }

        public GetPingMonitoringEntitiesQuery(long siteId)
        {
            SiteId = siteId;
        }
    }
}