using Autofac;
using M2c.Api.Application.Queries;
using M2c.Domain.AggregatesModel;
using M2c.Infrastructure.Repositories;

namespace M2c.Api.Infrastructure.AutofacModules
{
    public class ApplicationModule
        : Module
    {
        public ApplicationModule(string qconstr)
        {
            QueriesConnectionString = qconstr;
        }

        public string QueriesConnectionString { get; }

        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(_ => new CustomerQueries(QueriesConnectionString))
                .As<ICustomerQueries>()
                .InstancePerLifetimeScope();

            builder.RegisterType<CustomerRepository>()
                .As<ICustomerRepository>()
                .InstancePerLifetimeScope();
        }
    }
}