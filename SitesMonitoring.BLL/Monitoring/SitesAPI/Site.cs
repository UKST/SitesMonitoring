using System.Collections.Generic;
using SitesMonitoring.BLL.Data;

namespace SitesMonitoring.BLL.Monitoring.SitesAPI
{
    public class Site : EntityBase
    {
        public string Name { get; set; }

        public ICollection<MonitoringEntity> MonitoringEntities { get; set; }
    }
}