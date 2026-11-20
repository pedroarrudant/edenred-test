using EdenredTest.Application.Shared.Domain;
using MediatR;

namespace EdenredTest.Application.Features.InsertVehicle.Models
{
    public class InsertVehicleInput : IRequest<bool>
    {
        public required Vehicle Vehicle { get; init; }
    }
}
