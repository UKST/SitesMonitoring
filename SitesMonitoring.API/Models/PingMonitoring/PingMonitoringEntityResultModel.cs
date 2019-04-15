using System;

namespace SitesMonitoring.API.Models.PingMonitoring
{
    public sealed class PingMonitoringEntityResultModel
    {
        public long Id { get; set; }
        public int PeriodInMinutes { get; set; }
        public string Address { get; set; }
    }
}