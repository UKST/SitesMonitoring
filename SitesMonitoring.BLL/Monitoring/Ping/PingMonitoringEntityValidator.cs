using System;
using System.IO;

namespace SitesMonitoring.BLL.Monitoring.Ping
{
    public sealed class PingMonitoringEntityValidator : IMonitoringEntityValidator
    {
        public void Validate(MonitoringEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            
            if (entity.Type != MonitoringType.Ping)
                throw new InvalidDataException();
        }
    }
}