using EdenredTest.Application.Features.InsertClient.Models;
using EdenredTest.Application.Shared.Constants;
using EdenredTest.Application.Shared.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Text.RegularExpressions;

namespace EdenredTest.Application.Features.InsertClient.UseCase
{
    public class InsertClientUseCaseHandler : IRequestHandler<InsertClientInput, InsertClientOutput>
    {
        private readonly IClientsRepository _repository;
        private readonly ILogger<InsertClientUseCaseHandler> _logger;

        public InsertClientUseCaseHandler(IClientsRepository repository, ILogger<InsertClientUseCaseHandler> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<InsertClientOutput> Handle(InsertClientInput request, CancellationToken cancellationToken)
        {
            Regex EmailRegex = new(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.Compiled);

            if (String.IsNullOrWhiteSpace(request.Client.FullName))
            {
                var errorMessage = String.Format(Messages.Error.InvalidName);

                _logger.LogInformation($"[{nameof(InsertClientUseCaseHandler)}][{nameof(Handle)}] {errorMessage}");

                return new InsertClientOutput { Success = false, Message = errorMessage };
            }

            if (!EmailRegex.IsMatch(request.Client.Email ?? string.Empty))
            {
                var errorMessage = String.Format(Messages.Error.InvalidEmail, request.Client.FullName);

                _logger.LogInformation($"[{nameof(InsertClientUseCaseHandler)}][{nameof(Handle)}] {errorMessage}");

                return new InsertClientOutput { Success = false, Message = errorMessage };
            }

            var isIdentificationDocumentAlreadyRegistered = await _repository.GetClientByIdentificationDocument(request?.Client.IdentificationDocument, cancellationToken);

            if (isIdentificationDocumentAlreadyRegistered)
            {
                var errorMessage = String.Format(Messages.Error.IdentificationDocumentAlreadyInUse, request.Client.FullName);

                _logger.LogInformation($"[{nameof(InsertClientUseCaseHandler)}][{nameof(Handle)}] {errorMessage}");

                return new InsertClientOutput { Success = false, Message = errorMessage };
            }

            var success = await _repository.InsertClientAsync(request?.Client, cancellationToken);

            return new InsertClientOutput { Success = success };
        }
    }
}
