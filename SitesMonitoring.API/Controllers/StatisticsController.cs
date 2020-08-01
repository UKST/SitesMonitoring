using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SitesMonitoring.API.Models.Statistics;
using SitesMonitoring.BLL.Monitoring.StatisticAPI;

namespace SitesMonitoring.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatisticsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public StatisticsController(
            IMapper mapper,
            IMediator mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ICollection<SiteStatisticResultModel>> GetAll()
            => _mapper.Map<ICollection<SiteStatisticResultModel>>(
                await _mediator.Send(new GetStatisticsQuery()));
    }
}