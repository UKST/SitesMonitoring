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
    [Authorize]
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
            return Ok(_monitoringService.GetAllEntities(siteId)
                .Map<ICollection<PingMonitoringEntityResultModel>>(_mapper));
        }
        
        [HttpPost]
        public PingMonitoringEntityResultModel Post(int siteId, [FromBody] PingMonitoringEntityPostModel model)
        {
            return _monitoringService.CreateEntity(model.Map<PingMonitoringEntityPostModel, MonitoringEntity>(_mapper, entity => entity.SiteId = siteId ))
                .Map<PingMonitoringEntityResultModel>(_mapper);
        }
        
        [HttpDelete("{entityId}")]
        public void Delete(int siteId, int entityId)
        {
            _monitoringService.RemoveEntity(siteId, entityId);
        }
    }
}