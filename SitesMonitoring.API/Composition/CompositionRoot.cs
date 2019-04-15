using System.Net.NetworkInformation;
using Autofac;
using SitesMonitoring.BLL.Data;
using SitesMonitoring.BLL.Monitoring;
using SitesMonitoring.BLL.Monitoring.MonitoringWorker;
using SitesMonitoring.BLL.Monitoring.PingMonitoringAPI;
using SitesMonitoring.BLL.Monitoring.SitesAPI;
using SitesMonitoring.BLL.Monitoring.StatisticAPI;
using SitesMonitoring.BLL.Utils;
using SitesMonitoring.DAL;

namespace SitesMonitoring.API.Composition
{
    public class CompositionRoot : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            RegisterServices(builder);

            RegisterKeyedServices(builder);
            
            RegisterRepositories(builder);
        }

        private static void RegisterServices(ContainerBuilder builder)
        {
            builder.RegisterType<PingMonitoringService>().As<IMonitoringService>().InstancePerLifetimeScope();
            builder.RegisterType<DateTimeProvider>().As<IDateTimeProvider>().InstancePerLifetimeScope();
            builder.RegisterType<SitesService>().As<ISitesService>().InstancePerLifetimeScope();
            builder.RegisterType<SitesStatisticService>().As<ISitesStatisticService>().InstancePerLifetimeScope();
            builder.RegisterType<PingMonitoringRequest>().As<IMonitoringRequest<PingReply>>().InstancePerLifetimeScope();
            
            builder.RegisterType<MonitoringWorker>().As<IMonitoringWorker>().SingleInstance();
            builder.RegisterType<MonitoringSettings>().As<IMonitoringSettings>().SingleInstance();
        }

        private static void RegisterKeyedServices(ContainerBuilder builder)
        {
            builder.RegisterType<PingMonitoringProcess>()
                .As<IMonitoringProcess>()
                .WithMetadata<MonitoringProcessMetadata>(configuration =>
                    configuration.For(m => m.Type, MonitoringType.Ping)).InstancePerLifetimeScope();
            
            // replace with metadata or keyed service approach if statistic based on different monitors will be required
            builder.RegisterType<PingHealthStatusMapper>().As<IHealthStatusMapper>().InstancePerLifetimeScope();
            builder.RegisterType<LastPingSiteHealthCalculationStrategy>().As<ISiteHealthCalculationStrategy>()
                .InstancePerLifetimeScope();
            builder.RegisterType<PingMonitoringValidator>().As<IMonitoringValidator>()
                .InstancePerLifetimeScope();
        }
        
        private static void RegisterRepositories(ContainerBuilder builder)
        {
            builder.RegisterType<MonitoringEntityRepository>().As<IMonitoringEntityRepository>()
                .SingleInstance();
            builder.RegisterType<MonitoringResultRepository>().As<IMonitoringResultRepository>()
                .SingleInstance();
            builder.RegisterType<Repository<Site>>().As<IRepository<Site>>()
                .SingleInstance();
        } 
    }
}