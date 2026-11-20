using EdenredTest.Application.Features.GetAllClients.Models;
using EdenredTest.Application.Shared.Models;
using EdenredTest.Application.Shared.Repositories;
using MediatR;

namespace EdenredTest.Application.Features.GetAllClients.UseCase
{
    public class GetAllClientsUseCaseHandler : IRequestHandler<GetAllClientsInput, IEnumerable<Client>>
    {
        private readonly IClientsRepository _repository;

        public GetAllClientsUseCaseHandler(IClientsRepository repository)
        {
            _repository = repository;
        }

        public Task<IEnumerable<Client>> Handle(GetAllClientsInput request, CancellationToken cancellationToken)
        {
            return _repository.GetAllClientsAsync(cancellationToken);
        }
    }
}
