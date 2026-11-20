using EdenredTest.Application.Features.InsertVehicle.Models;
using EdenredTest.Application.Shared.Repositories;
using MediatR;

namespace EdenredTest.Application.Features.InsertVehicle.UseCase
{
    public class InsertVehicleUseCaseHandler : IRequestHandler<InsertVehicleInput, bool>
    {
        private readonly IVehiclesRepository _repository;

        public InsertVehicleUseCaseHandler(IVehiclesRepository repository)
        {
            _repository = repository;
        }

        public Task<bool> Handle(InsertVehicleInput request, CancellationToken cancellationToken)
        {
            return _repository.InsertVehicleAsync(request.Vehicle, cancellationToken);
        }
    }
}
