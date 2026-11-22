using Dapper;
using EdenredTest.Application.Shared.Domain;
using Microsoft.Extensions.Logging;
using System.Data;

namespace EdenredTest.Application.Shared.Repositories
{
    public class ClientsRepository : BaseSqlRepository, IClientsRepository
    {
        private readonly ILogger<ClientsRepository> _logger;
        public ClientsRepository(IDbConnection connection, ILogger<ClientsRepository> logger) : base(connection)
        {
            _logger = logger;
        }

        public async Task<Client> GetClientById(long Id, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"[{nameof(ClientsRepository)}][{nameof(GetClientById)}] Querying in the respository");

            try
            {
                var result = await base.QueryFirstOrDefault<Client>("SELECT_CLIENTBYID", cancellationToken, new { Id });

                _logger.LogInformation($"[{nameof(ClientsRepository)}][{nameof(GetClientById)}] Query concluded with success");

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[{nameof(ClientsRepository)}][{nameof(GetClientById)}] Error querying database");
                throw;
            }
        }

        public async Task<bool> InsertClientAsync(Client client, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"[{nameof(ClientsRepository)}][{nameof(InsertClientAsync)}] Querying in the respository");

            try
            {
                var parameters = CreateClientParameters(client);

                var result = await base.ExecuteAsync("INSERT_CLIENT", cancellationToken, parameters);

                _logger.LogInformation($"[{nameof(ClientsRepository)}][{nameof(InsertClientAsync)}] Query concluded with success");

                return result > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[{nameof(ClientsRepository)}][{nameof(InsertClientAsync)}] Error querying database");
                throw;
            }
        }

        public async Task<bool> UpdateClientAsync(Client client, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"[{nameof(ClientsRepository)}][{nameof(UpdateClientAsync)}] Querying in the respository");
            try
            {
                var parameters = CreateClientParameters(client);
                parameters.Add("Id", client.Id);

                var affectedRows = await base.ExecuteAsync("UPDATE_CLIENT", cancellationToken, parameters, retries: 0);

                _logger.LogInformation($"[{nameof(ClientsRepository)}][{nameof(UpdateClientAsync)}] Query concluded with success");
                return affectedRows > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[{nameof(ClientsRepository)}][{nameof(UpdateClientAsync)}] Error querying database");
                throw;
            }
        }

        public async Task<bool> DeleteClientAsync(long id, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"[{nameof(ClientsRepository)}][{nameof(DeleteClientAsync)}] Querying in the respository");
            try
            {
                var affectedRows = await base.ExecuteAsync("DELETE_CLIENT", cancellationToken, new { Id = id }, retries: 0);

                _logger.LogInformation($"[{nameof(ClientsRepository)}][{nameof(DeleteClientAsync)}] Query concluded with success");
                return affectedRows > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[{nameof(ClientsRepository)}][{nameof(DeleteClientAsync)}] Error querying database");
                throw;
            }
        }

        public async Task<IEnumerable<Client>> GetAllClientsAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation($"[{nameof(ClientsRepository)}][{nameof(GetAllClientsAsync)}] Querying the database");

            try
            {
                var clients = await base.QueryAsync<Client>("SELECT_CLIENTS", cancellationToken);

                _logger.LogInformation($"[{nameof(ClientsRepository)}][{nameof(GetAllClientsAsync)}] Query concluded with success");

                return clients;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[{nameof(ClientsRepository)}][{nameof(GetAllClientsAsync)}] Error querying database");
                throw;
            }
        }

        public async Task<bool> GetClientByIdentificationDocument(string identificationDocument, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"[{nameof(ClientsRepository)}][{nameof(GetClientByIdentificationDocument)}] Querying the database");

            try
            {
                var client = await base.QueryFirstOrDefault<Client>("SELECT_CLIENTBYCPF", cancellationToken, new { identificationDocument });

                _logger.LogInformation($"[{nameof(ClientsRepository)}][{nameof(GetClientByIdentificationDocument)}] Query concluded with success");

                if (client is null)
                {
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[{nameof(ClientsRepository)}][{nameof(GetClientByIdentificationDocument)}] Error querying database");
                throw;
            }
        }

        private static DynamicParameters CreateClientParameters(Client client)
        {
            var parameters = new DynamicParameters();
            parameters.Add("FullName", client.FullName);
            parameters.Add("IdentificationDocument", client.IdentificationDocument);
            parameters.Add("BirthDate", client.BirthDate.Date, DbType.Date);
            parameters.Add("Email", client.Email);
            parameters.Add("Phone", client.TelephoneNumber);
            return parameters;
        }
    }
}
