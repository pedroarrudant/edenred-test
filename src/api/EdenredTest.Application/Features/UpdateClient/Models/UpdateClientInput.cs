using MediatR;
using EdenredTest.Application.Shared.Models;

namespace EdenredTest.Application.Features.UpdateClient.Models
{
    public class UpdateClientInput : IRequest<bool>
    {
        public Client Client { get; init; } = null!;
    }
}
