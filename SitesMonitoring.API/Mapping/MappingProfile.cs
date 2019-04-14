using AutoMapper;
using SitesMonitoring.API.Models.PingMonitoring;
using SitesMonitoring.BLL.Monitoring;

namespace SitesMonitoring.API.Mapping
{
    public class MappingProfile : Profile {
        public MappingProfile() {
            CreateMap<PingMonitoringEntityPostModel, MonitoringEntity>();
            CreateMap<MonitoringEntity, PingMonitoringEntityResultModel>();
        }
    }
}