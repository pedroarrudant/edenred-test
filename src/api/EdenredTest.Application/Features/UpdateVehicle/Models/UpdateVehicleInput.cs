using EdenredTest.Application.Shared.Domain;
using EdenredTest.Application.Shared.Models;
using MediatR;

namespace EdenredTest.Application.Features.UpdateVehicle.Models
{
    public class UpdateVehicleInput : IRequest<VehicleWriteOutput>
    {
        public required Vehicle Vehicle { get; init; }
    }
}
