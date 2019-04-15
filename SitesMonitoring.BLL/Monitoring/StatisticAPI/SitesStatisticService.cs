using System.Collections.Generic;
using System.Linq;
using SitesMonitoring.BLL.Data;
using SitesMonitoring.BLL.Monitoring.SitesAPI;

namespace SitesMonitoring.BLL.Monitoring.StatisticAPI
{
    public class SitesStatisticService : ISitesStatisticService
    {
        private readonly IRepository<Site> _siteRepository;
        private readonly ISiteHealthCalculationStrategy _siteHealthCalculationStrategy;
        
        public SitesStatisticService(
            IRepository<Site> siteRepository,
            ISiteHealthCalculationStrategy siteHealthCalculationStrategy)
        {
            _siteRepository = siteRepository;
            _siteHealthCalculationStrategy = siteHealthCalculationStrategy;
        }
        
        public ICollection<SiteStatistic> GetStatistics()
        {
            // todo add paging
            var sites = _siteRepository.GetAll();

            return sites.Select(i =>
                    new SiteStatistic {SiteName = i.Name, SiteHealth = _siteHealthCalculationStrategy.GetHealth(i)})
                .ToArray();
        }
    }
}