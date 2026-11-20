using EdenredTest.Application.Shared.Constants;
using EdenredTest.Application.Shared.Domain;
using EdenredTest.Application.Shared.Repositories;
using EdenredTest.Application.Shared.Models;

namespace EdenredTest.Application.Shared.Services
{
    internal static class VehicleWriteValidation
    {
        private static readonly SemaphoreSlim WriteLock = new(1, 1);

        internal static async Task<VehicleWriteOutput> ValidateAndWriteAsync(
            IVehiclesRepository repository,
            Vehicle vehicle,
            long? excludedVehicleId,
            Func<Vehicle, CancellationToken, Task<bool>> writeAsync,
            CancellationToken cancellationToken)
        {
            if (vehicle.ManufactureYear > DateTime.Now.Year)
            {
                return new VehicleWriteOutput { Message = Messages.Error.InvalidManufactureYear };
            }

            await WriteLock.WaitAsync(cancellationToken);
            try
            {
                if (await repository.LicensePlateExistsAsync(vehicle.LicensePlate, excludedVehicleId, cancellationToken))
                {
                    return new VehicleWriteOutput { Message = Messages.Error.LicensePlateAlreadyInUse };
                }

                return new VehicleWriteOutput { Success = await writeAsync(vehicle, cancellationToken) };
            }
            finally
            {
                WriteLock.Release();
            }
        }
    }
}
