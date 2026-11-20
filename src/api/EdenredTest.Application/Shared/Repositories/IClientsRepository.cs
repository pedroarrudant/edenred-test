using EdenredTest.Application.Shared.Models;

namespace EdenredTest.Application.Shared.Repositories
{
    public interface IClientsRepository
    {
        public Task<Client> GetClientById(long Id, CancellationToken cancellationToken);
        public Task<IEnumerable<Client>> GetAllClientsAsync(CancellationToken cancellationToken);

        public Task<bool> InsertClientAsync(Client client, CancellationToken cancellationToken);
        public Task<bool> UpdateClientAsync(Client client, CancellationToken cancellationToken);
        public Task<bool> DeleteClientAsync(long id, CancellationToken cancellationToken);
    }
}
