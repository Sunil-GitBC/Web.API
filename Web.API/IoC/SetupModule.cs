using System.Collections.Generic;
using Autofac;
using Autofac.Features.Variance;
using MediatR;

namespace Web.API.IoC
{
    public class SetupModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(ThisAssembly)
                .AsImplementedInterfaces();

            // enables contravariant Resolve() for interfaces with single contravariant ("in") arg
            builder
                .RegisterSource(new ContravariantRegistrationSource());

            // mediator itself
            builder
                .RegisterType<Mediator>()
                .As<IMediator>()
                .InstancePerLifetimeScope();

            // request handlers
            builder
                .Register<SingleInstanceFactory>(context =>
                {
                    var ctx = context.Resolve<IComponentContext>(); // unsure why needed, but it works
                    return t => ctx.TryResolve(t, out var o) ? o : null;
                })
                .InstancePerLifetimeScope();

            // notification handlers
            builder
                .Register<MediatR.MultiInstanceFactory>(context =>
                {
                    var ctx = context.Resolve<IComponentContext>(); 
                    return t => (IEnumerable<object>)ctx.Resolve(typeof(IEnumerable<>).MakeGenericType(t));
                })
                .InstancePerLifetimeScope();
        }
    }
}
