using EdenredTest.Application.Features.InsertVehicle.Models;
using EdenredTest.Application.Features.InsertVehicle.UseCase;
using EdenredTest.Application.Shared.Constants;
using EdenredTest.Application.Shared.Domain;
using EdenredTest.Application.Shared.Repositories;
using EdenredTestTests.Static;
using Microsoft.Extensions.Logging;
using Moq;

namespace EdenredTestTests.Handlers
{
    public class InsertVehicleUseCaseHandlerTests
    {
        private readonly Mock<IVehiclesRepository> _repository = new();
        private readonly Mock<ILogger<InsertVehicleUseCaseHandler>> _logger = new();

        [Fact]
        public async Task Should_Reject_Vehicle_When_Plate_Is_Already_In_Use()
        {
            // Arrange
            var vehicle = VehicleStatic.Create();
            _repository.Setup(r => r.LicensePlateExistsAsync(vehicle.LicensePlate, null, CancellationToken.None))
                .ReturnsAsync(true);
            var input = new InsertVehicleInput { Vehicle = vehicle };
            var handler = new InsertVehicleUseCaseHandler(_repository.Object, _logger.Object);

            // Act
            var result = await handler.Handle(input, CancellationToken.None);

            // Assert
            Assert.False(result.Success);
            Assert.Equal(Messages.Error.LicensePlateAlreadyInUse, result.Message);
            _repository.Verify(r => r.InsertVehicleAsync(It.IsAny<Vehicle>(), CancellationToken.None), Times.Never);
        }

        [Fact]
        public async Task Should_Reject_Vehicle_With_Future_Manufacture_Year()
        {
            // Arrange
            var vehicle = VehicleStatic.Create();
            vehicle.ManufactureYear = 10000;
            var input = new InsertVehicleInput { Vehicle = vehicle };
            var handler = new InsertVehicleUseCaseHandler(_repository.Object, _logger.Object);

            // Act
            var result = await handler.Handle(input, CancellationToken.None);

            // Assert
            Assert.False(result.Success);
            Assert.Equal(Messages.Error.InvalidManufactureYear, result.Message);
            _repository.Verify(r => r.InsertVehicleAsync(It.IsAny<Vehicle>(), CancellationToken.None), Times.Never);
        }

        [Fact]
        public async Task Should_Reject_Vehicle_Without_A_Client()
        {
            var vehicle = VehicleStatic.Create();
            vehicle.ClientId = null;
            var handler = new InsertVehicleUseCaseHandler(_repository.Object, _logger.Object);

            var result = await handler.Handle(new InsertVehicleInput { Vehicle = vehicle }, CancellationToken.None);

            Assert.False(result.Success);
            Assert.Equal(Messages.Error.ClientRequired, result.Message);
            _repository.Verify(r => r.InsertVehicleAsync(It.IsAny<Vehicle>(), CancellationToken.None), Times.Never);
        }

        [Fact]
        public async Task Should_Reject_Vehicle_When_Client_Does_Not_Exist()
        {
            var vehicle = VehicleStatic.Create();
            _repository.Setup(r => r.InsertVehicleAsync(vehicle, CancellationToken.None)).ReturnsAsync(false);
            var handler = new InsertVehicleUseCaseHandler(_repository.Object, _logger.Object);

            var result = await handler.Handle(new InsertVehicleInput { Vehicle = vehicle }, CancellationToken.None);

            Assert.False(result.Success);
            Assert.Equal(Messages.Error.VehicleClientNotFound, result.Message);
        }

        [Fact]
        public async Task Should_Create_Vehicle_With_Success()
        {
            // Arrange
            var vehicle = VehicleStatic.Create();
            _repository.Setup(r => r.LicensePlateExistsAsync(vehicle.LicensePlate, null, CancellationToken.None))
                .ReturnsAsync(false);
            _repository.Setup(r => r.InsertVehicleAsync(vehicle, CancellationToken.None)).ReturnsAsync(true);
            var input = new InsertVehicleInput { Vehicle = vehicle };
            var handler = new InsertVehicleUseCaseHandler(_repository.Object, _logger.Object);

            // Act
            var result = await handler.Handle(input, CancellationToken.None);

            // Assert
            Assert.True(result.Success);
            _repository.Verify(r => r.InsertVehicleAsync(vehicle, CancellationToken.None), Times.Once);
        }
    }
}
