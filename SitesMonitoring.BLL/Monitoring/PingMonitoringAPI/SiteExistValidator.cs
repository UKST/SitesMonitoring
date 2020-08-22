using FluentValidation;
using SitesMonitoring.BLL.Data;
using SitesMonitoring.BLL.Monitoring.SitesAPI;

namespace SitesMonitoring.BLL.Monitoring.PingMonitoringAPI
{
    public class SiteExistValidator<T> : AbstractValidator<T> where T : ISiteId
    {
        public SiteExistValidator(IRepository<Site> siteRepository)
        {
            RuleFor(i => i.SiteId).MustAsync(async (siteId, token) =>
            {
                var entity = await siteRepository.GetFirstOrDefaultAsync(new GetByIdSpecification<Site>(siteId));
                return entity != null;
            }).WithMessage($"{nameof(Site)} not found");
        }
    }
}