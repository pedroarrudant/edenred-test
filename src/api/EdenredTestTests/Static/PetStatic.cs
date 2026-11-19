using System;
using EdenredTest.Application.Shared.Models;

namespace EdenredTestTests.Static
{
    public static class PetStatic
    {
        public static Pet Create() => new Pet() { Age = 10, Name = "Teste" };
    }
}

