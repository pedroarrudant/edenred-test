using Autofac;
using EdenredTest.Application.Shared.Middlewares;
using MediatR.Pipeline;
using System.Diagnostics.CodeAnalysis;

namespace EdenredTest.Application.Shared.AutofacModules
{
    [ExcludeFromCodeCoverage]
    public class ModuleHandler : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterGeneric(typeof(ExceptionHandler<>)).As(typeof(IRequestExceptionAction<,>));
        }
    }
}

