using System.Collections.Generic;
using Autofac;
using Autofac.Features.Variance;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Web.API.Auth;

namespace Web.API.IoC
{
    public class SetupModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(ThisAssembly)
                .AsImplementedInterfaces();

            builder.RegisterType<HasScopeHandler>().As<IAuthorizationHandler>().SingleInstance();

            // enables contravariant Resolve() for interfaces with single contravariant ("in") arg
            builder
                .RegisterSource(new ContravariantRegistrationSource());

            // mediator itself
            builder
                .RegisterType<Mediator>()
                .As<IMediator>()
                .InstancePerLifetimeScope();

            // request handlers
            builder.Register<ServiceFactory>(context =>
            {
                var componentContext = context.Resolve<IComponentContext>();
                return t =>
                {
                    object o;
                    return componentContext.TryResolve(t, out o) ? o : null;
                };
            });
        }
    }
}
