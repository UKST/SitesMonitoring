using System;

namespace SitesMonitoring.API.Models.PingMonitoring
{
    public class PingMonitoringPostModel
    {
        public TimeSpan Period { get; set; }
        public string Address { get; set; }
    }
}