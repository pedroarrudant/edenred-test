using EdenredTest.Application.Features.UpdateClient.Models;
using EdenredTest.Application.Shared.Repositories;
using MediatR;

namespace EdenredTest.Application.Features.UpdateClient.UseCase
{
    public class UpdateClientUseCaseHandler : IRequestHandler<UpdateClientInput, bool>
    {
        private readonly IClientsRepository _repository;

        public UpdateClientUseCaseHandler(IClientsRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(UpdateClientInput request, CancellationToken cancellationToken)
        {
            return await _repository.UpdateClientAsync(request.Client, cancellationToken);
        }
    }
}
