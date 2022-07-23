using System.Reflection;
using Autofac;
using M2c.Domain.SeedWork;
using M2c.Infrastructure.Repositories;

namespace M2c.Api.Infrastructure.AutofacModules
{
    public class ApplicationModule
        : Autofac.Module
    {
        public string QueriesConnectionString { get; }

        public ApplicationModule(string qconstr)
        {
            QueriesConnectionString = qconstr;
        }
        protected override void Load(ContainerBuilder builder)
        {

            builder
                .RegisterGeneric(typeof(Repository<>))
                .As(typeof(IRepository<>))
                .InstancePerLifetimeScope();

        }
    }
}