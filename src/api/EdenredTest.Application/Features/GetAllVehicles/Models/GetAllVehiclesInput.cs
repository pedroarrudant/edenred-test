using EdenredTest.Application.Shared.Domain;
using MediatR;

namespace EdenredTest.Application.Features.GetAllVehicles.Models
{
    public class GetAllVehiclesInput : IRequest<IEnumerable<Vehicle>>
    {
    }
}
