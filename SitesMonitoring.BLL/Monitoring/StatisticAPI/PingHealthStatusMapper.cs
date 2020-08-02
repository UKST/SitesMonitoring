using System;
using System.Net.NetworkInformation;
using SitesMonitoring.BLL.Monitoring.PingMonitoringAPI;

namespace SitesMonitoring.BLL.Monitoring.StatisticAPI
{
    public class PingHealthStatusMapper : IHealthStatusMapper
    {
        public SiteHealth Map(MonitoringResult result)
        {
            if (result == null)
                throw new ArgumentNullException(nameof(result));
            
            if (result.Data == null)
                throw new ArgumentNullException(nameof(result.Data));

            var typedData = result.GetData<PingMonitoringResultData>();

            return typedData.IpStatus == IPStatus.Success ? SiteHealth.Good : SiteHealth.Bad;
        }
    }
}