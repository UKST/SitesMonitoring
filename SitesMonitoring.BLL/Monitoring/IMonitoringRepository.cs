using System.Collections.Generic;
using SitesMonitoring.BLL.Data;

namespace SitesMonitoring.BLL.Monitoring
{
    public interface IMonitoringRepository<T> : IRepository<T>
    {
        IEnumerable<T> GetBySiteId(int siteId);
    }
}