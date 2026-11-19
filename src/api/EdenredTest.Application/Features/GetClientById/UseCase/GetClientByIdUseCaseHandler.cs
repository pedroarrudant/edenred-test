using EdenredTest.Application.Features.GetClientById.Models;
using EdenredTest.Application.Shared.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace EdenredTest.Application.Features.GetClientById.UseCase
{
    public class GetClientByIdUseCaseHandler : IRequestHandler<GetClientByIdInput, GetClientByIdOutput>
    {
        private readonly IClientsRepository _repository;
        private ILogger<GetClientByIdUseCaseHandler> _logger;

        public GetClientByIdUseCaseHandler(IClientsRepository repository, ILogger<GetClientByIdUseCaseHandler> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<GetClientByIdOutput> Handle(GetClientByIdInput request, CancellationToken cancellationToken)
        {
            var result = await _repository.GetClientById(request.Id, cancellationToken);

            return new GetClientByIdOutput() { client = result };
        }
    }
}
