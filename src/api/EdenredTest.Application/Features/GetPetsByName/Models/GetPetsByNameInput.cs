using System;
using MediatR;

namespace EdenredTest.Application.Features.GetPetsByName.Models
{
    public class GetPetsByNameInput : IRequest<GetPetsByNameOutput>
    {
        public string? Name { get; set; }
    }
}

