using EdenredTest.Application.Features.InsertClient.Models;
using EdenredTest.Application.Features.InsertClient.UseCase;
using EdenredTest.Application.Shared.Constants;
using EdenredTest.Application.Shared.Domain;
using EdenredTest.Application.Shared.Repositories;
using EdenredTestTests.Static;
using Microsoft.Extensions.Logging;
using Moq;

namespace EdenredTestTests.Handlers
{
    public class InsertClientUseCaseHandlerTests
    {
        private readonly Mock<IClientsRepository> _repository;
        private readonly Mock<ILogger<InsertClientUseCaseHandler>> _logger;

        public InsertClientUseCaseHandlerTests()
        {
            _repository = new Mock<IClientsRepository>();
            _logger = new Mock<ILogger<InsertClientUseCaseHandler>>();
        }

        [Fact]
        public async Task Should_Create_Client_With_Success()
        {
            //Arrange
            _repository.Setup(m => m.InsertClientAsync(It.IsAny<Client>(), new CancellationToken())).ReturnsAsync(true);

            var input = new InsertClientInput() { Client = ClientStatic.Create() };

            //Act
            var handler = new InsertClientUseCaseHandler(_repository.Object, _logger.Object);
            var result = await handler.Handle(new InsertClientInput() { Client = ClientStatic.Create() }, new CancellationToken());

            //Assert
            Assert.True(result.Success);
        }

        [Fact]
        public async Task Should_Reject_Client_With_Invalid_Email()
        {
            // Arrange
            var client = ClientStatic.Create();
            client.Email = "invalid-email";
            var input = new InsertClientInput { Client = client };
            var handler = new InsertClientUseCaseHandler(_repository.Object, _logger.Object);

            // Act
            var result = await handler.Handle(input, CancellationToken.None);

            // Assert
            Assert.False(result.Success);
            Assert.Equal(string.Format(Messages.Error.InvalidEmail, client.FullName), result.Message);
            _repository.Verify(r => r.InsertClientAsync(It.IsAny<Client>(), CancellationToken.None), Times.Never);
        }

        [Fact]
        public async Task Should_Reject_Client_When_Cpf_Is_Already_In_Use()
        {
            // Arrange
            var client = ClientStatic.Create();
            _repository.Setup(r => r.GetClientByIdentificationDocument(client.IdentificationDocument, CancellationToken.None))
                .ReturnsAsync(true);
            var input = new InsertClientInput { Client = client };
            var handler = new InsertClientUseCaseHandler(_repository.Object, _logger.Object);

            // Act
            var result = await handler.Handle(input, CancellationToken.None);

            // Assert
            Assert.False(result.Success);
            Assert.Equal(string.Format(Messages.Error.IdentificationDocumentAlreadyInUse, client.FullName), result.Message);
            _repository.Verify(r => r.InsertClientAsync(It.IsAny<Client>(), CancellationToken.None), Times.Never);
        }

        [Fact]
        public async Task Should_Reject_Client_With_No_Name()
        {
            // Arrange
            var client = ClientStatic.Create();
            client.FullName = string.Empty;
            var input = new InsertClientInput { Client = client };
            var handler = new InsertClientUseCaseHandler(_repository.Object, _logger.Object);

            // Act
            var result = await handler.Handle(input, CancellationToken.None);

            // Assert
            Assert.False(result.Success);
            Assert.Equal(Messages.Error.InvalidName, result.Message);
            _repository.Verify(r => r.InsertClientAsync(It.IsAny<Client>(), CancellationToken.None), Times.Never);
        }
    }
}
