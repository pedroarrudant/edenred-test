using EdenredTest.Application.Features.GetVehicleById.Models;
using EdenredTest.Application.Shared.Models;
using EdenredTest.Application.Shared.Repositories;
using MediatR;

namespace EdenredTest.Application.Features.GetVehicleById.UseCase
{
    public class GetVehicleByIdUseCaseHandler : IRequestHandler<GetVehicleByIdInput, Vehicle?>
    {
        private readonly IVehiclesRepository _repository;

        public GetVehicleByIdUseCaseHandler(IVehiclesRepository repository)
        {
            _repository = repository;
        }

        public Task<Vehicle?> Handle(GetVehicleByIdInput request, CancellationToken cancellationToken)
        {
            return _repository.GetVehicleById(request.Id, cancellationToken);
        }
    }
}
