using Autofac;
using SitesMonitoring.BLL.Data;
using SitesMonitoring.BLL.Monitoring;
using SitesMonitoring.BLL.Monitoring.MonitoringWorker;
using SitesMonitoring.BLL.Monitoring.Ping;
using SitesMonitoring.BLL.Utils;
using SitesMonitoring.DAL;

namespace SitesMonitoring.API
{
    public class CompositionRoot : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<PingMonitoringService>().As<IMonitoringService>().InstancePerLifetimeScope();
            builder.RegisterType<PingMonitoringEntityValidator>().As<IMonitoringEntityValidator>()
                .InstancePerLifetimeScope();
            builder.RegisterType<DateTimeProvider>().As<IDateTimeProvider>().InstancePerLifetimeScope();

            builder.RegisterType<MonitoringWorker>().As<IMonitoringWorker>().SingleInstance();
            builder.RegisterType<PingMonitoringProcess>()
                .As<IMonitoringProcess>()
                .WithMetadata<MonitoringProcessMetadata>(configuration =>
                    configuration.For(m => m.Type, MonitoringType.Ping)).InstancePerLifetimeScope();
            builder.RegisterType<MonitoringSettings>().As<IMonitoringSettings>().SingleInstance();
            
            builder.RegisterType<MonitoringEntityRepository>().As<IMonitoringEntityRepository>()
                .SingleInstance();
            builder.RegisterType<Repository<MonitoringResult>>().As<IRepository<MonitoringResult>>()
                .SingleInstance();
        }
    }
}