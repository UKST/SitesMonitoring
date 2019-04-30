using System;
using System.Text.RegularExpressions;
using SitesMonitoring.BLL.Data;
using SitesMonitoring.BLL.ErrorHandling;
using SitesMonitoring.BLL.Monitoring.SitesAPI;

namespace SitesMonitoring.BLL.Monitoring.PingMonitoringAPI
{
    public sealed class PingMonitoringValidator : IMonitoringValidator
    {
        private const string ValidHostnameRegex = @"^(([a-zA-Z0-9]|[a-zA-Z0-9][a-zA-Z0-9\-]*[a-zA-Z0-9])\.)*([A-Za-z0-9]|[A-Za-z0-9][A-Za-z0-9\-]*[A-Za-z0-9])$";
        
        private readonly IMonitoringSettings _monitoringSettings;
        private readonly IRepository<Site> _siteRepository;
        
        public PingMonitoringValidator(
            IRepository<Site> siteRepository,
            IMonitoringSettings monitoringSettings)
        {
            _monitoringSettings = monitoringSettings;
            _siteRepository = siteRepository;
        }
        
        public void ValidateEntity(MonitoringEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            ValidateSiteExistence(entity.SiteId);
            
            if (entity.Type != MonitoringType.Ping)
                throw new ArgumentException($"{nameof(entity)} must have {nameof(entity.Type)}: {MonitoringType.Ping}");

            var validPeriods = _monitoringSettings.CombinedMonitoringPeriodsInMinutes;

            if (!validPeriods.Contains(entity.PeriodInMinutes))
                throw new ApplicationValidationException(
                    $"{nameof(entity.PeriodInMinutes)} should have one of the following values: {string.Join(", ", validPeriods)}");

            if (!Regex.IsMatch(entity.Address, ValidHostnameRegex))
                throw new ApplicationValidationException($"{nameof(entity.Address)} should be valid DNS address");
        }

        public void ValidateSiteExistence(long siteId)
        {
            var site = _siteRepository.GetById(siteId);

            if (site == null)
                throw new NotFoundException();
        }
    }
}