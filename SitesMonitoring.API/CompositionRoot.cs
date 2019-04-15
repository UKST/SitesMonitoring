using Autofac;
using SitesMonitoring.BLL.Data;
using SitesMonitoring.BLL.Monitoring;
using SitesMonitoring.BLL.Monitoring.MonitoringWorker;
using SitesMonitoring.BLL.Monitoring.PingMonitoringAPI;
using SitesMonitoring.BLL.Monitoring.SitesAPI;
using SitesMonitoring.BLL.Monitoring.StatisticAPI;
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
            builder.RegisterType<SitesService>().As<ISitesService>().InstancePerLifetimeScope();
            builder.RegisterType<SitesStatisticService>().As<ISitesStatisticService>().InstancePerLifetimeScope();
            // replace with metadata or keyed service approach if statistic based on different monitors will be required
            builder.RegisterType<PingHealthStatusMapper>().As<IHealthStatusMapper>().InstancePerLifetimeScope();
            builder.RegisterType<LastPingSiteHealthCalculationStrategy>().As<ISiteHealthCalculationStrategy>()
                .InstancePerLifetimeScope();
            
            builder.RegisterType<MonitoringWorker>().As<IMonitoringWorker>().SingleInstance();
            builder.RegisterType<PingMonitoringProcess>()
                .As<IMonitoringProcess>()
                .WithMetadata<MonitoringProcessMetadata>(configuration =>
                    configuration.For(m => m.Type, MonitoringType.Ping)).InstancePerLifetimeScope();
            builder.RegisterType<MonitoringSettings>().As<IMonitoringSettings>().SingleInstance();
            
            builder.RegisterType<MonitoringEntityRepository>().As<IMonitoringEntityRepository>()
                .SingleInstance();
            builder.RegisterType<MonitoringResultRepository>().As<IMonitoringResultRepository>()
                .SingleInstance();
            builder.RegisterType<Repository<Site>>().As<IRepository<Site>>()
                .SingleInstance();
        }
    }
}