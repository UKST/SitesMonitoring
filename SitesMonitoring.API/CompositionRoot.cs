using Autofac;
using AutoMapper;
using SitesMonitoring.BLL.Data;
using SitesMonitoring.BLL.Endpoints;
using SitesMonitoring.BLL.Monitoring;
using SitesMonitoring.BLL.Monitoring.Ping;
using SitesMonitoring.DAL;

namespace SitesMonitoring.API
{
    public class CompositionRoot : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<Handler>().As<IHandler>().InstancePerLifetimeScope();
            builder.RegisterType<PingMonitoringService>().As<IMonitoringService>().InstancePerLifetimeScope();
            builder.RegisterType<PingMonitoringEntityValidator>().As<IMonitoringEntityValidator>()
                .InstancePerLifetimeScope();
            
            builder.RegisterType<Repository>().As<IRepository<string>>().InstancePerLifetimeScope();
            builder.RegisterType<MonitoringEntityRepository>().As<IMonitoringRepository<MonitoringEntity>>()
                .SingleInstance();
        }
    }
}