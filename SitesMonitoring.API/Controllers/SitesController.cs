using AutoMapper;
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
    [Authorize]
    public class SitesController : ControllerBase
    {
        private readonly ISitesService _sitesService;
        private readonly IMapper _mapper;
        
        public SitesController(
            ISitesService sitesService,
            IMapper mapper)
        {
            _sitesService = sitesService;
            _mapper = mapper;
        }
        
        [HttpPost]
        public ActionResult<SiteResultModel> Post([FromBody] SitePostModel model)
        {
            var entity = _mapper.Map<Site>(model);
            var result = _sitesService.CreateSite(entity);

            return _mapper.Map<SiteResultModel>(result);
        }
    }
}