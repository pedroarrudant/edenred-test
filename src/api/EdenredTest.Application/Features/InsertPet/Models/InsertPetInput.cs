using System;
using EdenredTest.Application.Features.InsertPet.Models;
using EdenredTest.Application.Shared.Models;
using MediatR;

namespace EdenredTest.Application.Features.InsertPet.Models
{
    public class InsertPetInput : IRequest<InsertPetOutput>
    {
        public Pet? Pet { get; set; }
    }
}

