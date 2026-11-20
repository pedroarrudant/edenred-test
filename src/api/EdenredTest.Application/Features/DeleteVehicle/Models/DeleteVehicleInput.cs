using MediatR;

namespace EdenredTest.Application.Features.DeleteVehicle.Models
{
    public class DeleteVehicleInput : IRequest<bool>
    {
        public long Id { get; init; }
    }
}
