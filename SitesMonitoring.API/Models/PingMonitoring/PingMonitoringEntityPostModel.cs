using System;

namespace SitesMonitoring.API.Models.PingMonitoring
{
    public class PingMonitoringEntityPostModel
    {
        public int PeriodInMinutes { get; set; }
        public string Address { get; set; }
    }
}