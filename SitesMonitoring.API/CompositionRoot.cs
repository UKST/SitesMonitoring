using Autofac;
using SitesMonitoring.BLL.Data;
using SitesMonitoring.BLL.Endpoints;
using SitesMonitoring.DAL;

namespace SitesMonitoring.API
{
    public class CompositionRoot : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<Handler>().As<IHandler>().InstancePerLifetimeScope();
            builder.RegisterType<Repository>().As<IRepository>().InstancePerLifetimeScope();
        }
    }
}