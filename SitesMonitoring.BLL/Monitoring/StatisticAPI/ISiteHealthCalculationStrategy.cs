using System.Threading.Tasks;
using SitesMonitoring.BLL.Monitoring.SitesAPI;

namespace SitesMonitoring.BLL.Monitoring.StatisticAPI
{
    public interface ISiteHealthCalculationStrategy
    {
        Task<SiteHealth> GetHealthAsync(Site site);
    }
}