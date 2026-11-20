using EdenredTest.Application.Shared.Domain;
using EdenredTest.Application.Shared.Models;
using MediatR;

namespace EdenredTest.Application.Features.InsertClient.Models
{
    public class InsertClientInput : IRequest<InsertClientOutput>
    {
        public required Client Client { get; set; }
    }
}
