using EdenredTest.Application.Shared.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace EdenredTestTests.Static
{
    public static class ClientStatic
    {
        public static Client Create() => new Client()
        {
            Id = 1,
            FullName = "Test Name",
            BirthDate = new DateTime(1990, 1, 1),
            Email = "test@test.com.br",
            IdentificationDocument = "11111111111",
            TelephoneNumber = "11941515123"
        };
    }
}
