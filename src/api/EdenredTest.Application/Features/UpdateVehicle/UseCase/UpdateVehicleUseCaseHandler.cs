using EdenredTest.Application.Features.UpdateVehicle.Models;
using EdenredTest.Application.Shared.Repositories;
using MediatR;

namespace EdenredTest.Application.Features.UpdateVehicle.UseCase
{
    public class UpdateVehicleUseCaseHandler : IRequestHandler<UpdateVehicleInput, bool>
    {
        private readonly IVehiclesRepository _repository;

        public UpdateVehicleUseCaseHandler(IVehiclesRepository repository)
        {
            _repository = repository;
        }

        public Task<bool> Handle(UpdateVehicleInput request, CancellationToken cancellationToken)
        {
            return _repository.UpdateVehicleAsync(request.Vehicle, cancellationToken);
        }
    }
}
