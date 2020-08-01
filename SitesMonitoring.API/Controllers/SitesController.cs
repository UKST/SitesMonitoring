using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SitesMonitoring.API.Models.Sites;
using SitesMonitoring.BLL.Monitoring.SitesAPI;

namespace SitesMonitoring.API.Controllers
{
    /// <summary>
    /// Includes operations for setup site. Uses minimal methods amount required for working with monitoring statistics.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class SitesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public SitesController(
            IMapper mapper,
            IMediator mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<SiteResultModel> Post([FromBody] SitePostModel model)
        => _mapper.Map<SiteResultModel>(
            await _mediator.Send(new CreateSiteCommand(
                _mapper.Map<Site>(model))));
    }
}