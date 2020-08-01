using System.Collections.Generic;
using MediatR;

namespace SitesMonitoring.BLL.Monitoring.StatisticAPI
{
    public class GetStatisticsQuery : IRequest<ICollection<SiteStatistic>>
    {
    }
}