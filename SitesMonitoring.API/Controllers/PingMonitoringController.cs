using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SitesMonitoring.API.Models.PingMonitoring;
using SitesMonitoring.BLL.Monitoring;
using SitesMonitoring.BLL.Monitoring.PingMonitoringAPI;
using SitesMonitoring.BLL.Monitoring.PingMonitoringAPI.Create;
using SitesMonitoring.BLL.Monitoring.PingMonitoringAPI.Get;
using SitesMonitoring.BLL.Monitoring.PingMonitoringAPI.Remove;

namespace SitesMonitoring.API.Controllers
{
    /// <summary>
    /// Includes operations for setup ping monitor. PUT omitted for simplicity
    /// </summary>
    [Route("api/sites/{siteId}/[controller]")]
    [ApiController]
    public class PingMonitoringController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public PingMonitoringController(
            IMediator mediator,
            IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ICollection<PingMonitoringEntityResultModel>> GetAll(long siteId)
            => _mapper.Map<ICollection<PingMonitoringEntityResultModel>>(
                await _mediator.Send(new GetPingMonitoringEntitiesQuery(siteId)));

        [HttpPost]
        public async Task<PingMonitoringEntityResultModel> Post(int siteId, [FromBody] PingMonitoringEntityPostModel model)
            => _mapper.Map<PingMonitoringEntityResultModel>(
                await _mediator.Send(new CreatePingMonitoringEntityCommand(
                    _mapper.Map<PingMonitoringEntityPostModel, MonitoringEntity>(model,
                        options => options.AfterMap(
                            (s, d) => { d.SiteId = siteId; })))));

        [HttpDelete("{entityId}")]
        public async Task Delete(int siteId, int entityId)
            => await _mediator.Send(new RemoveMonitoringEntityCommand(siteId, entityId));
    }
}