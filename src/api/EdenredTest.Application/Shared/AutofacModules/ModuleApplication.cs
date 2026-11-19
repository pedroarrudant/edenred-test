using Autofac;
using EdenredTest.Application.Entrypoint.Observer;
using EdenredTest.Application.Shared.Configuration;
using EdenredTest.Application.Shared.Repositories;
using EdenredTest.Application.Shared.Services;
using MassTransit;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Npgsql;
using System.Diagnostics.CodeAnalysis;

namespace EdenredTest.Application.Shared.AutofacModules
{
    [ExcludeFromCodeCoverage]
    public class ModuleApplication : Autofac.Module
    {
        protected async override void Load(ContainerBuilder builder)
        {
            builder.Register(container =>
            {
                var databaseOptions = container.Resolve<IOptionsSnapshot<DatabaseOptions>>();
                var logger = container.Resolve<ILogger<PetsRepository>>();

                var connection = new NpgsqlConnection(databaseOptions.Value.ConnectionString);
                return new PetsRepository(connection, logger);
            }).As<IPetsRepository>();

            builder.Register(container =>
            {
                var databaseOptions = container.Resolve<IOptionsSnapshot<DatabaseOptions>>();
                var logger = container.Resolve<ILogger<ClientsRepository>>();

                var connection = new NpgsqlConnection(databaseOptions.Value.ConnectionString);
                return new ClientsRepository(connection, logger);
            }).As<IClientsRepository>();

            _ = builder.Register(container =>
            {
                var logger = container.Resolve<ILogger<ReceiveObserver>>();
                var publish = container.Resolve<PublishService>();

                return new ReceiveObserver(logger, publish);
            }).As<IReceiveObserver>();
        }
    }
}

