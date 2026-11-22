using Autofac;
using MediatR;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace EdenredTest.Application.Shared.AutofacModules
{
    [ExcludeFromCodeCoverage]
    public class ModuleMediator : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(IMediator).GetTypeInfo().Assembly).AsImplementedInterfaces();

            builder.Register<ServiceFactory>(context =>
            {
                var componentContext = context.Resolve<IComponentContext>();
                return t => { return componentContext.TryResolve(t, out object o) ? o : null; };
            });
        }
    }
}

