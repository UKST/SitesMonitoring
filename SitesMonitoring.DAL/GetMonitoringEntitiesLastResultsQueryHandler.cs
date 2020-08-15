using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SitesMonitoring.BLL.Monitoring;
using SitesMonitoring.BLL.Monitoring.StatisticAPI;

namespace SitesMonitoring.DAL
{
    public class GetMonitoringEntitiesLastResultsQueryHandler : IRequestHandler<GetMonitoringEntitiesLastResultsQuery, IEnumerable<MonitoringResult>>
    {
        private readonly SitesMonitoringDbContext _dbContext;

        public GetMonitoringEntitiesLastResultsQueryHandler(SitesMonitoringDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<MonitoringResult>> Handle(GetMonitoringEntitiesLastResultsQuery request, CancellationToken cancellationToken)
        {
            return await _dbContext.MonitoringEntities
                .Where(i => i.SiteId == request.SiteId)
                .Select(i => i.MonitoringResults.OrderByDescending(k => k.Id).FirstOrDefault())
                .Where(i => i != null)
                .ToArrayAsync(cancellationToken);
        }
    }
}