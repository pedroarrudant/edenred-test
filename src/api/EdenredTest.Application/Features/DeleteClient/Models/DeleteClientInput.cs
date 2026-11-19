using MediatR;

namespace EdenredTest.Application.Features.DeleteClient.Models
{
    public class DeleteClientInput : IRequest<bool>
    {
        public long Id { get; init; }
    }
}
