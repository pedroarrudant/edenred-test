using EdenredTest.Application.Shared.Domain;
using MediatR;

namespace EdenredTest.Application.Features.GetVehicleById.Models
{
    public class GetVehicleByIdInput : IRequest<Vehicle?>
    {
        public long Id { get; init; }
    }
}
