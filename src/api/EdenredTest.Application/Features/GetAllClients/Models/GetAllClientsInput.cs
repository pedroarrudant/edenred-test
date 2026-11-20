using EdenredTest.Application.Shared.Models;
using MediatR;

namespace EdenredTest.Application.Features.GetAllClients.Models
{
    public class GetAllClientsInput : IRequest<IEnumerable<Client>>
    {
    }
}
