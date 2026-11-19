using MediatR;

namespace EdenredTest.Application.Features.GetClientById.Models
{
    public class GetClientByIdInput : IRequest<GetClientByIdOutput>
    {
        public long Id { get; set; }
    }
}
