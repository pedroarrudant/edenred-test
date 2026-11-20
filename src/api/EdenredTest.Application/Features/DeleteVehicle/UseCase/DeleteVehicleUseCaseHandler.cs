using EdenredTest.Application.Features.DeleteVehicle.Models;
using EdenredTest.Application.Shared.Repositories;
using MediatR;

namespace EdenredTest.Application.Features.DeleteVehicle.UseCase
{
    public class DeleteVehicleUseCaseHandler : IRequestHandler<DeleteVehicleInput, bool>
    {
        private readonly IVehiclesRepository _repository;

        public DeleteVehicleUseCaseHandler(IVehiclesRepository repository)
        {
            _repository = repository;
        }

        public Task<bool> Handle(DeleteVehicleInput request, CancellationToken cancellationToken)
        {
            return _repository.DeleteVehicleAsync(request.Id, cancellationToken);
        }
    }
}
