using System.Net.NetworkInformation;
using System.Text;

namespace SitesMonitoring.BLL.Monitoring.MonitoringWorker
{
    public class PingMonitoringProcess : IMonitoringProcess
    {
        private const int Timeout = 1024;
        private const string BufferString = "32 bites data buffer            ";
        
        public void Start(MonitoringEntity entity)
        {
            var pingSender = new System.Net.NetworkInformation.Ping();
            var options = new PingOptions
            {
                DontFragment = true
            };
    
            var buffer = Encoding.ASCII.GetBytes(BufferString);

            var reply = pingSender.Send(entity.Address, Timeout, buffer, options);
        }
    }
}