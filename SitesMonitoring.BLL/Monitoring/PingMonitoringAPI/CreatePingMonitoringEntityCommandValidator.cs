using System.Text.RegularExpressions;
using FluentValidation;
using SitesMonitoring.BLL.Monitoring.PingMonitoringAPI.Create;

namespace SitesMonitoring.BLL.Monitoring.PingMonitoringAPI
{
    public class CreatePingMonitoringEntityCommandValidator : AbstractValidator<CreatePingMonitoringEntityCommand>
    {
        private const string ValidHostnameRegex = @"^(([a-zA-Z0-9]|[a-zA-Z0-9][a-zA-Z0-9\-]*[a-zA-Z0-9])\.)*([A-Za-z0-9]|[A-Za-z0-9][A-Za-z0-9\-]*[A-Za-z0-9])$";

        public CreatePingMonitoringEntityCommandValidator(IMonitoringSettings monitoringSettings, IValidator<ISiteId> siteIdValidator)
        {
            RuleFor(i => i.Entity).NotNull();

            RuleFor(i => i.Entity.Type).Equal(MonitoringType.Ping).WithMessage(
                $"{nameof(MonitoringEntity)} must have {nameof(MonitoringEntity.Type)}: {MonitoringType.Ping}");

            RuleFor(i => i.Entity).ChildRules(i => i.Include(siteIdValidator));

            var validPeriods = monitoringSettings.CombinedMonitoringPeriodsInMinutes;

            RuleFor(i => i.Entity.PeriodInMinutes)
                .Must(i => monitoringSettings.CombinedMonitoringPeriodsInMinutes.Contains(i))
                .WithMessage(
                    $"{nameof(MonitoringEntity.PeriodInMinutes)} should have one of the following values: {string.Join(", ", validPeriods)}");

            RuleFor(i => i.Entity.Address)
                .Must(i => Regex.IsMatch(i, ValidHostnameRegex))
                .WithMessage($"{nameof(MonitoringEntity.Address)} should be valid DNS address");
        }
    }
}