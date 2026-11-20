using EdenredTest.Application.Features.GetAllVehicles.Models;
using EdenredTest.Application.Shared.Models;
using EdenredTest.Application.Shared.Repositories;
using MediatR;

namespace EdenredTest.Application.Features.GetAllVehicles.UseCase
{
    public class GetAllVehiclesUseCaseHandler : IRequestHandler<GetAllVehiclesInput, IEnumerable<Vehicle>>
    {
        private readonly IVehiclesRepository _repository;

        public GetAllVehiclesUseCaseHandler(IVehiclesRepository repository)
        {
            _repository = repository;
        }

        public Task<IEnumerable<Vehicle>> Handle(GetAllVehiclesInput request, CancellationToken cancellationToken)
        {
            return _repository.GetAllVehiclesAsync(cancellationToken);
        }
    }
}
