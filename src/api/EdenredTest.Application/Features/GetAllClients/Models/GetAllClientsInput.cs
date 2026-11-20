using EdenredTest.Application.Shared.Domain;
using MediatR;

namespace EdenredTest.Application.Features.GetAllClients.Models
{
    public class GetAllClientsInput : IRequest<IEnumerable<Client>>
    {
    }
}
