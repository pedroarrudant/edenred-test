using EdenredTest.Application.Features.InsertClient.UseCase;
using EdenredTest.Application.Features.UpdateClient.Models;
using EdenredTest.Application.Shared.Constants;
using EdenredTest.Application.Shared.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Text.RegularExpressions;

namespace EdenredTest.Application.Features.UpdateClient.UseCase
{
    public class UpdateClientUseCaseHandler : IRequestHandler<UpdateClientInput, UpdatedClientOutput>
    {
        private readonly IClientsRepository _repository;
        private readonly ILogger<UpdateClientUseCaseHandler> _logger;

        public UpdateClientUseCaseHandler(IClientsRepository repository, ILogger<UpdateClientUseCaseHandler> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<UpdatedClientOutput> Handle(UpdateClientInput request, CancellationToken cancellationToken)
        {
            if (String.IsNullOrWhiteSpace(request.Client.FullName))
            {
                var errorMessage = String.Format(Messages.Error.InvalidName);

                _logger.LogInformation($"[{nameof(InsertClientUseCaseHandler)}][{nameof(Handle)}] {errorMessage}");

                return new UpdatedClientOutput { Success = false, Message = errorMessage };
            }

            Regex EmailRegex = new(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.Compiled);

            if (!EmailRegex.IsMatch(request.Client.Email ?? string.Empty))
            {
                var errorMessage = String.Format(Messages.Error.InvalidEmail, request.Client.FullName);

                _logger.LogInformation($"[{nameof(InsertClientUseCaseHandler)}][{nameof(Handle)}] {errorMessage}");

                return new UpdatedClientOutput { Success = false, Message = errorMessage };
            }

            var currentClientData = await _repository.GetClientById(request.Client.Id, cancellationToken);

            var isIdentificationDocumentAlreadyRegistered = await _repository.GetClientByIdentificationDocument(request?.Client.IdentificationDocument, cancellationToken);

            if ((currentClientData.IdentificationDocument != request.Client.IdentificationDocument) && isIdentificationDocumentAlreadyRegistered)
            {
                var errorMessage = String.Format(Messages.Error.IdentificationDocumentAlreadyInUse, request.Client.FullName);

                _logger.LogInformation($"[{nameof(InsertClientUseCaseHandler)}][{nameof(Handle)}] {errorMessage}");

                return new UpdatedClientOutput { Success = false, Message = errorMessage };
            }

            var result = await _repository.UpdateClientAsync(request.Client, cancellationToken);

            return new UpdatedClientOutput { Success = result };
        }
    }
}
