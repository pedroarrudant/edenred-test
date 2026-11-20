using EdenredTest.Application.Features.UpdateVehicle.Models;
using EdenredTest.Application.Shared.Repositories;
using EdenredTest.Application.Shared.Models;
using EdenredTest.Application.Shared.Services;
using MediatR;

namespace EdenredTest.Application.Features.UpdateVehicle.UseCase
{
    public class UpdateVehicleUseCaseHandler : IRequestHandler<UpdateVehicleInput, VehicleWriteOutput>
    {
        private readonly IVehiclesRepository _repository;

        public UpdateVehicleUseCaseHandler(IVehiclesRepository repository)
        {
            _repository = repository;
        }

        public Task<VehicleWriteOutput> Handle(UpdateVehicleInput request, CancellationToken cancellationToken)
        {
            return VehicleWriteValidation.ValidateAndWriteAsync(
                _repository, request.Vehicle, request.Vehicle.Id, _repository.UpdateVehicleAsync, cancellationToken);
        }
    }
}
