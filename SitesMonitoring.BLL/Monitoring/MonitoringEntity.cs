using System;

namespace SitesMonitoring.BLL.Monitoring
{
    public class MonitoringEntity
    {
        public int Id { get; set; }
        public MonitoringType Type { get; set; }
        public TimeSpan Period { get; set; }
        public string Address { get; set; }
        public int SiteId { get; set; }
    }
}