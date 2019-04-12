using System;

namespace SitesMonitoring.API.Models.PingMonitoring
{
    // todo maybe should make generic for all monitors
    public sealed class PingMonitoringResult
    {
        public int Id { get; set; }
        public TimeSpan Period { get; set; }
        public string Address { get; set; }
    }
}