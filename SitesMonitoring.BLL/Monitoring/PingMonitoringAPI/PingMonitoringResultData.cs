using System.Net.NetworkInformation;

namespace SitesMonitoring.BLL.Monitoring.PingMonitoringAPI
{
    public class PingMonitoringResultData
    {
        public IPStatus IpStatus { get; }

        public PingMonitoringResultData(IPStatus ipStatus)
        {
            IpStatus = ipStatus;
        }
    }
}