using System;
using SitesMonitoring.BLL.Data;

namespace SitesMonitoring.BLL.Monitoring
{
    public class MonitoringResult : EntityBase
    {
        public object Data { get; set; }
        public DateTime CreatedDate { get; set; }
        public long MonitoringEntityId { get; set; }
    }
}