using EdenredTest.Application.Shared.Domain;
using Microsoft.Extensions.Logging;
using System.Data;

namespace EdenredTest.Application.Shared.Repositories
{
    public class VehiclesRepository : BaseSqlRepository, IVehiclesRepository
    {
        private readonly ILogger<VehiclesRepository> _logger;

        public VehiclesRepository(IDbConnection connection, ILogger<VehiclesRepository> logger) : base(connection)
        {
            _logger = logger;
        }

        public async Task<Vehicle?> GetVehicleById(long id, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"[{nameof(VehiclesRepository)}][{nameof(GetVehicleById)}] Querying the database");

            try
            {
                var vehicle = await QueryFirstOrDefault<Vehicle?>("SELECT_VEHICLEBYID", cancellationToken, new { Id = id });

                _logger.LogInformation($"[{nameof(VehiclesRepository)}][{nameof(GetVehicleById)}] Query concluded with success");
                return vehicle;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[{nameof(VehiclesRepository)}][{nameof(GetVehicleById)}] Error querying database");
                throw;
            }
        }

        public async Task<IEnumerable<Vehicle>> GetAllVehiclesAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation($"[{nameof(VehiclesRepository)}][{nameof(GetAllVehiclesAsync)}] Querying the database");

            try
            {
                var vehicles = await QueryAsync<Vehicle>("SELECT_VEHICLES", cancellationToken);

                _logger.LogInformation($"[{nameof(VehiclesRepository)}][{nameof(GetAllVehiclesAsync)}] Query concluded with success");
                return vehicles;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[{nameof(VehiclesRepository)}][{nameof(GetAllVehiclesAsync)}] Error querying database");
                throw;
            }
        }

        public Task<bool> LicensePlateExistsAsync(string licensePlate, long? excludedVehicleId, CancellationToken cancellationToken)
        {
            return QueryFirstOrDefault<bool>(
                "SELECT_VEHICLE_LICENSE_PLATE_EXISTS",
                cancellationToken,
                new { LicensePlate = licensePlate, ExcludedVehicleId = excludedVehicleId });
        }

        public async Task<bool> InsertVehicleAsync(Vehicle vehicle, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"[{nameof(VehiclesRepository)}][{nameof(InsertVehicleAsync)}] Querying the database");

            try
            {
                var affectedRows = await ExecuteAsync("INSERT_VEHICLE", cancellationToken, vehicle, retries: 0);

                _logger.LogInformation($"[{nameof(VehiclesRepository)}][{nameof(InsertVehicleAsync)}] Query concluded with success");
                return affectedRows > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[{nameof(VehiclesRepository)}][{nameof(InsertVehicleAsync)}] Error querying database");
                throw;
            }
        }

        public async Task<bool> UpdateVehicleAsync(Vehicle vehicle, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"[{nameof(VehiclesRepository)}][{nameof(UpdateVehicleAsync)}] Querying the database");

            try
            {
                var affectedRows = await ExecuteAsync("UPDATE_VEHICLE", cancellationToken, vehicle, retries: 0);

                _logger.LogInformation($"[{nameof(VehiclesRepository)}][{nameof(UpdateVehicleAsync)}] Query concluded with success");
                return affectedRows > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[{nameof(VehiclesRepository)}][{nameof(UpdateVehicleAsync)}] Error querying database");
                throw;
            }
        }

        public async Task<bool> DeleteVehicleAsync(long id, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"[{nameof(VehiclesRepository)}][{nameof(DeleteVehicleAsync)}] Querying the database");

            try
            {
                var affectedRows = await ExecuteAsync("DELETE_VEHICLE", cancellationToken, new { Id = id }, retries: 0);

                _logger.LogInformation($"[{nameof(VehiclesRepository)}][{nameof(DeleteVehicleAsync)}] Query concluded with success");
                return affectedRows > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[{nameof(VehiclesRepository)}][{nameof(DeleteVehicleAsync)}] Error querying database");
                throw;
            }
        }
    }
}
