using System.Net.NetworkInformation;
using System.Text;

namespace SitesMonitoring.BLL.Monitoring.PingMonitoringAPI
{
    public class PingMonitoringRequest : IMonitoringRequest<PingReply>
    {
        private const int Timeout = 1024;
        private const string BufferString = "32 bites data buffer            ";
        
        public PingReply Send(MonitoringEntity entity)
        {
            var pingSender = new Ping();
            var options = new PingOptions
            {
                DontFragment = true
            };
    
            var buffer = Encoding.ASCII.GetBytes(BufferString);

            return pingSender.Send(entity.Address, Timeout, buffer, options);
        }
    }
}