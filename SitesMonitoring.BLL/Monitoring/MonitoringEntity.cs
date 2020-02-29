using System.Collections.Generic;
using SitesMonitoring.BLL.Data;
using SitesMonitoring.BLL.Monitoring.SitesAPI;

namespace SitesMonitoring.BLL.Monitoring
{
    public class MonitoringEntity : EntityBase
    {
        public MonitoringType Type { get; set; }
        public int PeriodInMinutes { get; set; }
        public string Address { get; set; }
        public long SiteId { get; set; }

        public Site Site { get; set; }
        
        public ICollection<MonitoringResult> MonitoringResults { get; set; }
    }
}