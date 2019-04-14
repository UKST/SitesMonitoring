using System;
using SitesMonitoring.BLL.Data;

namespace SitesMonitoring.BLL.Monitoring
{
    public class MonitoringEntity : EntityBase
    {
        public MonitoringType Type { get; set; }
        public int PeriodInMinutes { get; set; }
        public string Address { get; set; }
        public int SiteId { get; set; }
    }
}