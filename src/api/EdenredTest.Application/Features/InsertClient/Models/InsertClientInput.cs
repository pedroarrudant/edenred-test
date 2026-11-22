using EdenredTest.Application.Shared.Domain;
using MediatR;

namespace EdenredTest.Application.Features.InsertClient.Models
{
    public class InsertClientInput : IRequest<InsertClientOutput>
    {
        public required Client Client { get; set; }
    }
}
