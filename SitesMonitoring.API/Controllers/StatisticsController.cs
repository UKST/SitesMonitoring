using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SitesMonitoring.API.Models.Statistics;
using SitesMonitoring.BLL.Monitoring.StatisticAPI;

namespace SitesMonitoring.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatisticsController : ControllerBase
    {
        private readonly ISitesStatisticService _sitesStatisticService;
        private readonly IMapper _mapper;
        
        public StatisticsController(
            ISitesStatisticService sitesStatisticService,
            IMapper mapper)
        {
            _sitesStatisticService = sitesStatisticService;
            _mapper = mapper;
        }
        
        [HttpGet]
        public ActionResult<ICollection<SiteStatisticResultModel>> GetAll()
        {
            var result = _sitesStatisticService.GetStatistics();
            
            return Ok(_mapper.Map<ICollection<SiteStatisticResultModel>>(result));
        }
    }
}