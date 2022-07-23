using System.Reflection;
using Autofac;
using FluentValidation;
using M2c.Api.Application.Behaviors;
using M2c.Api.Application.Commands.CustomerCommands.Create;
using MediatR;
using Module = Autofac.Module;

namespace M2c.Api.Infrastructure.AutofacModules
{
    public class MediatorModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(IMediator).GetTypeInfo().Assembly)
                .AsImplementedInterfaces();

            // Register all the Command classes (they implement IRequestHandler) in assembly holding the Commands
            builder.RegisterAssemblyTypes(typeof(CreateCustomerCommand).GetTypeInfo().Assembly)
                .AsClosedTypesOf(typeof(IRequestHandler<,>));




            // Register the Command's Validators (Validators based on FluentValidation library)
            builder
                 .RegisterAssemblyTypes(typeof(CreateCustomerCommandValidator).GetTypeInfo().Assembly)
                 .Where(t => t.IsClosedTypeOf(typeof(IValidator<>)))
                 .AsImplementedInterfaces();



            builder.Register<ServiceFactory>(context =>
            {
                var componentContext = context.Resolve<IComponentContext>();
                return t =>
                {
                    object o;
                    return componentContext.TryResolve(t, out o) ? o : null;
                };
            });

            builder.RegisterGeneric(typeof(LoggingBehavior<,>)).As(typeof(IPipelineBehavior<,>));
            builder.RegisterGeneric(typeof(ValidatorBehavior<,>)).As(typeof(IPipelineBehavior<,>));
        }
    }
}