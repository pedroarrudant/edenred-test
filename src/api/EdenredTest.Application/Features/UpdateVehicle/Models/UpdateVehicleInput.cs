using EdenredTest.Application.Shared.Domain;
using MediatR;

namespace EdenredTest.Application.Features.UpdateVehicle.Models
{
    public class UpdateVehicleInput : IRequest<bool>
    {
        public required Vehicle Vehicle { get; init; }
    }
}
