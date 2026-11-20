using EdenredTest.Application.Shared.Models;
using MediatR;

namespace EdenredTest.Application.Features.GetVehicleById.Models
{
    public class GetVehicleByIdInput : IRequest<Vehicle?>
    {
        public long Id { get; init; }
    }
}
