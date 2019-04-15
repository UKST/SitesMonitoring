namespace SitesMonitoring.BLL.Monitoring
{
    public interface IMonitoringValidator
    {
        void ValidateEntity(MonitoringEntity entity);
        void ValidateSiteExistence(long siteId);
    }
}