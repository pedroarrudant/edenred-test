using System;
using System.Diagnostics.CodeAnalysis;
using EdenredTest.Application.Shared.Configuration;
using EdenredTest.Application.Shared.Services;
using Autofac;

namespace EdenredTest.Application.Shared.AutofacModules
{
    [ExcludeFromCodeCoverage]
    public class ModuleInternalServices : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<PublishService>();

            //builder.RegisterType<InternalService>().As<IINternalService>.SingleInstance();
            //builder.RegisterType<Options>();
        }
    }
}

