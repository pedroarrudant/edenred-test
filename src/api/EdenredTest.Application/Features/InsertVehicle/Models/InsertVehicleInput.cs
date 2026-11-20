using EdenredTest.Application.Shared.Domain;
using EdenredTest.Application.Shared.Models;
using MediatR;

namespace EdenredTest.Application.Features.InsertVehicle.Models
{
    public class InsertVehicleInput : IRequest<VehicleWriteOutput>
    {
        public required Vehicle Vehicle { get; init; }
    }
}
