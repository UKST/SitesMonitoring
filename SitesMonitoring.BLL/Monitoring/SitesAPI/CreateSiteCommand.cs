using MediatR;

namespace SitesMonitoring.BLL.Monitoring.SitesAPI
{
    public class CreateSiteCommand : IRequest<Site>
    {
        public Site Site { get; }

        public CreateSiteCommand(Site site)
        {
            Site = site;
        }
    }
}