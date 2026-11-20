using EdenredTest.Application.Features.InsertVehicle.Models;
using EdenredTest.Application.Shared.Repositories;
using EdenredTest.Application.Shared.Models;
using EdenredTest.Application.Shared.Services;
using MediatR;

namespace EdenredTest.Application.Features.InsertVehicle.UseCase
{
    public class InsertVehicleUseCaseHandler : IRequestHandler<InsertVehicleInput, VehicleWriteOutput>
    {
        private readonly IVehiclesRepository _repository;

        public InsertVehicleUseCaseHandler(IVehiclesRepository repository)
        {
            _repository = repository;
        }

        public Task<VehicleWriteOutput> Handle(InsertVehicleInput request, CancellationToken cancellationToken)
        {
            return VehicleWriteValidation.ValidateAndWriteAsync(
                _repository, request.Vehicle, null, _repository.InsertVehicleAsync, cancellationToken);
        }
    }
}
