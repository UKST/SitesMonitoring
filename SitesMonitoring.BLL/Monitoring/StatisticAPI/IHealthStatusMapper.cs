namespace SitesMonitoring.BLL.Monitoring.StatisticAPI
{
    public interface IHealthStatusMapper
    {
        SiteHealth Map(MonitoringResult result);
    }
}