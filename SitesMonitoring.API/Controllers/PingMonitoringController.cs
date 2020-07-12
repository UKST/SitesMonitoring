using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SitesMonitoring.API.Mapping;
using SitesMonitoring.API.Models.PingMonitoring;
using SitesMonitoring.BLL.Monitoring;

namespace SitesMonitoring.API.Controllers
{
    /// <summary>
    /// Includes operations for setup ping monitor. PUT omitted for simplicity
    /// </summary>
    [Route("api/sites/{siteId}/[controller]")]
    [ApiController]
    public class PingMonitoringController : ControllerBase
    {
        private readonly IMonitoringService _monitoringService;
        private readonly IMapper _mapper;

        public PingMonitoringController(
            IMonitoringService monitoringService,
            IMapper mapper)
        {
            _monitoringService = monitoringService;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<ICollection<PingMonitoringEntityResultModel>> GetAll(int siteId)
        {
            var result = _monitoringService.GetAllEntities(siteId);

            return Ok(_mapper.Map<ICollection<PingMonitoringEntityResultModel>>(result));
        }

        [HttpPost]
        public PingMonitoringEntityResultModel Post(int siteId, [FromBody] PingMonitoringEntityPostModel model)
        {
            var entity = _mapper.Map<PingMonitoringEntityPostModel, MonitoringEntity>(model,
                options => options.AfterMap(
                    (s, d) => { d.SiteId = siteId; }));

            var result = _monitoringService.CreateEntity(entity);

            return _mapper.Map<PingMonitoringEntityResultModel>(result);
        }

        [HttpDelete("{entityId}")]
        public void Delete(int siteId, int entityId)
        {
            _monitoringService.RemoveEntity(siteId, entityId);
        }
    }
}