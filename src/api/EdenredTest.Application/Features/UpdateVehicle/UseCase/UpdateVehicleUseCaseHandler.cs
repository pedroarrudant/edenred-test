using EdenredTest.Application.Features.UpdateVehicle.Models;
using EdenredTest.Application.Shared.Constants;
using EdenredTest.Application.Shared.Models;
using EdenredTest.Application.Shared.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace EdenredTest.Application.Features.UpdateVehicle.UseCase
{
    public class UpdateVehicleUseCaseHandler : IRequestHandler<UpdateVehicleInput, VehicleWriteOutput>
    {
        private readonly IVehiclesRepository _repository;
        private readonly ILogger<UpdateVehicleUseCaseHandler> _logger;

        public UpdateVehicleUseCaseHandler(IVehiclesRepository repository, ILogger<UpdateVehicleUseCaseHandler> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<VehicleWriteOutput> Handle(UpdateVehicleInput request, CancellationToken cancellationToken)
        {
            var vehicle = request.Vehicle;

            if (vehicle.ManufactureYear > DateTime.Now.Year)
            {
                var errorMessage = Messages.Error.InvalidManufactureYear;
                _logger.LogInformation("[{Handler}][{Method}] {ErrorMessage}", nameof(UpdateVehicleUseCaseHandler), nameof(Handle), errorMessage);

                return new VehicleWriteOutput { Success = false, Message = errorMessage };
            }

            var licensePlateExists = await _repository.LicensePlateExistsAsync(vehicle.LicensePlate, vehicle.Id, cancellationToken);
            if (licensePlateExists)
            {
                var errorMessage = Messages.Error.LicensePlateAlreadyInUse;
                _logger.LogInformation("[{Handler}][{Method}] {ErrorMessage}", nameof(UpdateVehicleUseCaseHandler), nameof(Handle), errorMessage);

                return new VehicleWriteOutput { Success = false, Message = errorMessage };
            }

            var success = await _repository.UpdateVehicleAsync(vehicle, cancellationToken);
            return new VehicleWriteOutput { Success = success };
        }
    }
}
