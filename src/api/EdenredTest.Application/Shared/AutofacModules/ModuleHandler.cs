using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using EdenredTest.Application.Features.GetPetsByName.UseCase;
using EdenredTest.Application.Features.InsertPet.UseCase;
using EdenredTest.Application.Shared.Middlewares;
using Autofac;
using MediatR;
using MediatR.Pipeline;

namespace EdenredTest.Application.Shared.AutofacModules
{
    [ExcludeFromCodeCoverage]
    public class ModuleHandler : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterGeneric(typeof(ExceptionHandler<>)).As(typeof(IRequestExceptionAction<,>));

            builder.RegisterAssemblyTypes(typeof(GetPetsByNameUseCaseHandler).GetTypeInfo().Assembly).AsClosedTypesOf(typeof(IRequestHandler<,>));

            builder.RegisterAssemblyTypes(typeof(InsertPetUseCaseHandler).GetTypeInfo().Assembly).AsClosedTypesOf(typeof(IRequestHandler<,>));
        }
    }
}

