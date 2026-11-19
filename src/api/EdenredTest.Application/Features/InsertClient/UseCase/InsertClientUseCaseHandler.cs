using EdenredTest.Application.Features.InsertClient.Models;
using EdenredTest.Application.Shared.Repositories;
using MediatR;

namespace EdenredTest.Application.Features.InsertClient.UseCase
{
    public class InsertClientUseCaseHandler : IRequestHandler<InsertClientInput, InsertClientOutput>
    {
        private readonly IClientsRepository _repository;

        public InsertClientUseCaseHandler(IClientsRepository repository)
        {
            _repository = repository;
        }

        public async Task<InsertClientOutput> Handle(InsertClientInput request, CancellationToken cancellationToken)
        {
            var success = await _repository.InsertClientAsync(request.Client, cancellationToken);

            return new InsertClientOutput { Success = success };
        }
    }
}
