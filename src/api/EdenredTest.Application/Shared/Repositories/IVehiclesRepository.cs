using EdenredTest.Application.Shared.Domain;

namespace EdenredTest.Application.Shared.Repositories
{
    public interface IVehiclesRepository
    {
        Task<Vehicle?> GetVehicleById(long id, CancellationToken cancellationToken);
        Task<IEnumerable<Vehicle>> GetAllVehiclesAsync(CancellationToken cancellationToken);
        Task<bool> InsertVehicleAsync(Vehicle vehicle, CancellationToken cancellationToken);
        Task<bool> UpdateVehicleAsync(Vehicle vehicle, CancellationToken cancellationToken);
        Task<bool> DeleteVehicleAsync(long id, CancellationToken cancellationToken);
    }
}
