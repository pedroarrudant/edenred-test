using Autofac;
using EdenredTest.Application.Shared.Configuration;
using EdenredTest.Application.Shared.Repositories;
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
                var logger = container.Resolve<ILogger<ClientsRepository>>();

                var connection = new NpgsqlConnection(databaseOptions.Value.ConnectionString);
                return new ClientsRepository(connection, logger);
            }).As<IClientsRepository>();

            builder.Register(container =>
            {
                var databaseOptions = container.Resolve<IOptionsSnapshot<DatabaseOptions>>();
                var connection = new NpgsqlConnection(databaseOptions.Value.ConnectionString);
                return new VehiclesRepository(connection);
            }).As<IVehiclesRepository>();
        }
    }
}

