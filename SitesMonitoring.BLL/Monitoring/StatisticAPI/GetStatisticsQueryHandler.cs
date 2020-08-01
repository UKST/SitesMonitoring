using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SitesMonitoring.BLL.Data;
using SitesMonitoring.BLL.Monitoring.SitesAPI;

namespace SitesMonitoring.BLL.Monitoring.StatisticAPI
{
    public class GetStatisticsQueryHandler : IRequestHandler<GetStatisticsQuery, ICollection<SiteStatistic>>
    {
        private readonly IRepository<Site> _siteRepository;
        private readonly ISiteHealthCalculationStrategy _siteHealthCalculationStrategy;

        public GetStatisticsQueryHandler(
            IRepository<Site> siteRepository,
            ISiteHealthCalculationStrategy siteHealthCalculationStrategy)
        {
            _siteRepository = siteRepository;
            _siteHealthCalculationStrategy = siteHealthCalculationStrategy;
        }

        public Task<ICollection<SiteStatistic>> Handle(GetStatisticsQuery request, CancellationToken cancellationToken)
        {
            // todo add paging
            var sites = _siteRepository.GetAll();

            return Task.FromResult<ICollection<SiteStatistic>>(sites.Select(i =>
                    new SiteStatistic {SiteName = i.Name, SiteHealth = _siteHealthCalculationStrategy.GetHealth(i)})
                .ToArray());
        }
    }
}