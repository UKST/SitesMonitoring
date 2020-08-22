using MediatR;

namespace SitesMonitoring.BLL.Monitoring.PingMonitoringAPI.Remove
{
    public class RemoveMonitoringEntityCommand : IRequest, ISiteId
    {
        public long SiteId { get; }

        public int Id { get; }

        public RemoveMonitoringEntityCommand(long siteId, int id)
        {
            SiteId = siteId;
            Id = id;
        }
    }
}