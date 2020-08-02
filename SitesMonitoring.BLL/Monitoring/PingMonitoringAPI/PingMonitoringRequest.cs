using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace SitesMonitoring.BLL.Monitoring.PingMonitoringAPI
{
    public class PingMonitoringRequest : IMonitoringRequest<PingReply>
    {
        private const int Timeout = 1024;
        private const string BufferString = "32 bites data buffer            ";

        public async Task<PingReply> SendAsync(MonitoringEntity entity)
        {
            var pingSender = new Ping();
            var options = new PingOptions
            {
                DontFragment = true
            };

            var buffer = Encoding.ASCII.GetBytes(BufferString);

            return await pingSender.SendPingAsync(entity.Address, Timeout, buffer, options);
        }
    }
}