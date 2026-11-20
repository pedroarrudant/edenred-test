using EdenredTest.Application.Shared.Models;
using MediatR;

namespace EdenredTest.Application.Features.GetAllVehicles.Models
{
    public class GetAllVehiclesInput : IRequest<IEnumerable<Vehicle>>
    {
    }
}
