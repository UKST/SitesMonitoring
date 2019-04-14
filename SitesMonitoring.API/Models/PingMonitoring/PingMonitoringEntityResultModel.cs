using System;

namespace SitesMonitoring.API.Models.PingMonitoring
{
    public sealed class PingMonitoringEntityResultModel
    {
        public int Id { get; set; }
        public int PeriodInMinutes { get; set; }
        public string Address { get; set; }
    }
}