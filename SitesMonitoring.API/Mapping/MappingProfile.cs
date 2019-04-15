using AutoMapper;
using SitesMonitoring.API.Models.PingMonitoring;
using SitesMonitoring.API.Models.Sites;
using SitesMonitoring.API.Models.Statistics;
using SitesMonitoring.BLL.Monitoring;
using SitesMonitoring.BLL.Monitoring.SitesAPI;
using SitesMonitoring.BLL.Monitoring.StatisticAPI;
using SiteHealthAPI = SitesMonitoring.API.Models.Statistics.SiteHealth;
using SiteHealthBLL = SitesMonitoring.BLL.Monitoring.StatisticAPI.SiteHealth;

namespace SitesMonitoring.API.Mapping
{
    public class MappingProfile : Profile {
        public MappingProfile() {
            CreateMap<PingMonitoringEntityPostModel, MonitoringEntity>();
            CreateMap<MonitoringEntity, PingMonitoringEntityResultModel>();
            CreateMap<SiteStatistic, SiteStatisticResultModel>();
            CreateMap<SiteHealthBLL, SiteHealthAPI>();
            CreateMap<SitePostModel, Site>();
            CreateMap<Site, SiteResultModel>();
        }
    }
}