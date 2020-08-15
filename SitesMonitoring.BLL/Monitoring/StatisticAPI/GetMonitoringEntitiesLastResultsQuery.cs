using System.Collections.Generic;
using MediatR;

namespace SitesMonitoring.BLL.Monitoring.StatisticAPI
{
    public class GetMonitoringEntitiesLastResultsQuery : IRequest<IEnumerable<MonitoringResult>>
    {
        public long SiteId { get; }

        public GetMonitoringEntitiesLastResultsQuery(long siteId)
        {
            SiteId = siteId;
        }
    }
}