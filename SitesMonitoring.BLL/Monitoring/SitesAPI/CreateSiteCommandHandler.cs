using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SitesMonitoring.BLL.Data;

namespace SitesMonitoring.BLL.Monitoring.SitesAPI
{
    public class CreateSiteCommandHandler : IRequestHandler<CreateSiteCommand, Site>
    {
        private readonly IRepository<Site> _siteRepository;

        public CreateSiteCommandHandler(IRepository<Site> siteRepository)
        {
            _siteRepository = siteRepository;
        }

        public Task<Site> Handle(CreateSiteCommand request, CancellationToken cancellationToken)
        {
            _siteRepository.Create(request.Site);

            return Task.FromResult(request.Site);
        }
    }
}