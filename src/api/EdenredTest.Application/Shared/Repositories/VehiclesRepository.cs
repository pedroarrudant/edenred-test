using EdenredTest.Application.Shared.Models;
using System.Data;

namespace EdenredTest.Application.Shared.Repositories
{
    public class VehiclesRepository : BaseSqlRepository, IVehiclesRepository
    {
        public VehiclesRepository(IDbConnection connection) : base(connection)
        {
        }

        public Task<Vehicle?> GetVehicleById(long id, CancellationToken cancellationToken)
        {
            return QueryFirstOrDefault<Vehicle?>("SELECT_VEHICLEBYID", cancellationToken, new { Id = id });
        }

        public Task<IEnumerable<Vehicle>> GetAllVehiclesAsync(CancellationToken cancellationToken)
        {
            return QueryAsync<Vehicle>("SELECT_VEHICLES", cancellationToken);
        }

        public async Task<bool> InsertVehicleAsync(Vehicle vehicle, CancellationToken cancellationToken)
        {
            var affectedRows = await ExecuteAsync("INSERT_VEHICLE", cancellationToken, vehicle, retries: 0);
            return affectedRows > 0;
        }

        public async Task<bool> UpdateVehicleAsync(Vehicle vehicle, CancellationToken cancellationToken)
        {
            var affectedRows = await ExecuteAsync("UPDATE_VEHICLE", cancellationToken, vehicle, retries: 0);
            return affectedRows > 0;
        }

        public async Task<bool> DeleteVehicleAsync(long id, CancellationToken cancellationToken)
        {
            var affectedRows = await ExecuteAsync("DELETE_VEHICLE", cancellationToken, new { Id = id }, retries: 0);
            return affectedRows > 0;
        }
    }
}
