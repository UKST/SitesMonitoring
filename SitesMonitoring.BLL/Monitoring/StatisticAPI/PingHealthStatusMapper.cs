using System;
using System.Net.NetworkInformation;

namespace SitesMonitoring.BLL.Monitoring.StatisticAPI
{
    public class PingHealthStatusMapper : IHealthStatusMapper
    {
        public SiteHealth Map(object resultData)
        {
            if (resultData == null)
                throw new ArgumentNullException(nameof(resultData));

            var typedData = resultData as IPStatus?;

            if (typedData == null)
                throw new ArgumentException($"{nameof(resultData)} must have {nameof(IPStatus)} type");

            return typedData == IPStatus.Success ? SiteHealth.Good : SiteHealth.Bad;
        }
    }
}