using EdenredTest.Application.Features.DeleteClient.Models;
using EdenredTest.Application.Shared.Repositories;
using MediatR;

namespace EdenredTest.Application.Features.DeleteClient.UseCase
{
    public class DeleteClientUseCaseHandler : IRequestHandler<DeleteClientInput, bool>
    {
        private readonly IClientsRepository _repository;

        public DeleteClientUseCaseHandler(IClientsRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(DeleteClientInput request, CancellationToken cancellationToken)
        {
            return await _repository.DeleteClientAsync(request.Id, cancellationToken);
        }
    }
}
