using SitesMonitoring.BLL.Data;

namespace SitesMonitoring.BLL.Monitoring.SitesAPI
{
    public class SitesService : ISitesService
    {
        private readonly IRepository<Site> _siteRepository;
        
        public SitesService(IRepository<Site> siteRepository)
        {
            _siteRepository = siteRepository;
        }
        
        public Site CreateSite(Site site)
        {
            _siteRepository.Create(site);

            return site;
        }
    }
}