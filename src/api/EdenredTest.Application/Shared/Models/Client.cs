using System.Text.Json.Serialization;

namespace EdenredTest.Application.Shared.Models
{
    public class Client
    {
        public long Id { get; set; }
        public string FullName { get; set; }
        public string IdentificationDocument { get; set; }
        public DateTime BirthDate { get; set; }
        public string Email { get; set; }
        public string TelephoneNumber { get; set; }
    }
}
