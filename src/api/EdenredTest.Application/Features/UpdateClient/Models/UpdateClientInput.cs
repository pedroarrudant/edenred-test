using EdenredTest.Application.Shared.Domain;
using MediatR;

namespace EdenredTest.Application.Features.UpdateClient.Models
{
    public class UpdateClientInput : IRequest<UpdatedClientOutput>
    {
        public Client Client { get; init; } = null!;
    }
}
