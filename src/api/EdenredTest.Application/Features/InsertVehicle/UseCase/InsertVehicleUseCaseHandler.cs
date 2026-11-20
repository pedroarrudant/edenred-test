using EdenredTest.Application.Features.InsertVehicle.Models;
using EdenredTest.Application.Shared.Constants;
using EdenredTest.Application.Shared.Models;
using EdenredTest.Application.Shared.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace EdenredTest.Application.Features.InsertVehicle.UseCase
{
    public class InsertVehicleUseCaseHandler : IRequestHandler<InsertVehicleInput, VehicleWriteOutput>
    {
        private readonly IVehiclesRepository _repository;
        private readonly ILogger<InsertVehicleUseCaseHandler> _logger;

        public InsertVehicleUseCaseHandler(IVehiclesRepository repository, ILogger<InsertVehicleUseCaseHandler> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<VehicleWriteOutput> Handle(InsertVehicleInput request, CancellationToken cancellationToken)
        {
            var vehicle = request.Vehicle;

            if (vehicle.ManufactureYear > DateTime.Now.Year)
            {
                var errorMessage = Messages.Error.InvalidManufactureYear;
                _logger.LogInformation("[{Handler}][{Method}] {ErrorMessage}", nameof(InsertVehicleUseCaseHandler), nameof(Handle), errorMessage);

                return new VehicleWriteOutput { Success = false, Message = errorMessage };
            }

            var licensePlateExists = await _repository.LicensePlateExistsAsync(vehicle.LicensePlate, null, cancellationToken);
            if (licensePlateExists)
            {
                var errorMessage = Messages.Error.LicensePlateAlreadyInUse;
                _logger.LogInformation("[{Handler}][{Method}] {ErrorMessage}", nameof(InsertVehicleUseCaseHandler), nameof(Handle), errorMessage);

                return new VehicleWriteOutput { Success = false, Message = errorMessage };
            }

            var success = await _repository.InsertVehicleAsync(vehicle, cancellationToken);
            return new VehicleWriteOutput { Success = success };
        }
    }
}
