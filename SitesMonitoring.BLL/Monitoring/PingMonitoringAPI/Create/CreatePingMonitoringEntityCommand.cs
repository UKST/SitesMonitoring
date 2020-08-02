using MediatR;

namespace SitesMonitoring.BLL.Monitoring.PingMonitoringAPI.Create
{
    public class CreatePingMonitoringEntityCommand : IRequest<MonitoringEntity>
    {
        public MonitoringEntity Entity { get; }

        public CreatePingMonitoringEntityCommand(MonitoringEntity entity)
        {
            Entity = entity;
        }
    }
}