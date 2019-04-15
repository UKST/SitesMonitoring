using SitesMonitoring.BLL.Monitoring.SitesAPI;

namespace SitesMonitoring.BLL.Monitoring.StatisticAPI
{
    public interface ISiteHealthCalculationStrategy
    {
        SiteHealth GetHealth(Site site);
    }
}