using System.Net.NetworkInformation;
using Autofac;
using MediatR;
using Microsoft.Extensions.Hosting;
using SitesMonitoring.API.HostedServices;
using SitesMonitoring.BLL.Data;
using SitesMonitoring.BLL.Monitoring;
using SitesMonitoring.BLL.Monitoring.MonitoringWorker;
using SitesMonitoring.BLL.Monitoring.PingMonitoringAPI;
using SitesMonitoring.BLL.Monitoring.PingMonitoringAPI.Get;
using SitesMonitoring.BLL.Monitoring.PingMonitoringAPI.Remove;
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
            ReqisterCQRS(builder);

            RegisterServices(builder);

            RegisterKeyedServices(builder);

            RegisterRepositories(builder);
        }

        private static void ReqisterCQRS(ContainerBuilder builder)
        {
            builder
                .RegisterType<Mediator>()
                .As<IMediator>()
                .InstancePerLifetimeScope();

            builder.Register<ServiceFactory>(context =>
            {
                var c = context.Resolve<IComponentContext>();
                return t => c.Resolve(t);
            });

            builder.RegisterType<CreateSiteCommandHandler>().AsImplementedInterfaces().InstancePerDependency();
            builder.RegisterType<RemoveMonitoringEntityCommandHandler>().AsImplementedInterfaces().InstancePerDependency();
            builder.RegisterType<CreatePingMonitoringEntityCommandHandler>().AsImplementedInterfaces().InstancePerDependency();
            builder.RegisterType<GetPingMonitoringEntitiesQueryHandler>().AsImplementedInterfaces().InstancePerDependency();
            builder.RegisterType<GetStatisticsQueryHandler>().AsImplementedInterfaces().InstancePerDependency();
        }

        private static void RegisterServices(ContainerBuilder builder)
        {
            builder.RegisterType<DateTimeProvider>().As<IDateTimeProvider>().InstancePerLifetimeScope();
            builder.RegisterType<PingMonitoringRequest>().As<IMonitoringRequest<PingReply>>().InstancePerLifetimeScope();
            builder.RegisterType<MonitoringWorker>().As<IMonitoringWorker>().SingleInstance();
            builder.RegisterType<MonitoringSettings>().As<IMonitoringSettings>().SingleInstance();
            builder.RegisterType<MonitoringPeriodsProvider>().As<IMonitoringPeriodsProvider>()
                .InstancePerLifetimeScope();
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
                .InstancePerLifetimeScope();
            builder.RegisterType<MonitoringResultRepository>().As<IMonitoringResultRepository>()
                .InstancePerLifetimeScope();
            builder.RegisterType<Repository<Site>>().As<IRepository<Site>>().InstancePerLifetimeScope();
        }
    }
}