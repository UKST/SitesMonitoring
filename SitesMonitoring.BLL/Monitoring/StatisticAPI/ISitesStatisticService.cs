using System.Collections.Generic;

namespace SitesMonitoring.BLL.Monitoring.StatisticAPI
{
    public interface ISitesStatisticService
    {
        ICollection<SiteStatistic> GetStatistics();
    }
}