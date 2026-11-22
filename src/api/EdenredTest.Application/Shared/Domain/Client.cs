namespace EdenredTest.Application.Shared.Domain
{
    public class Client
    {
        public long Id { get; set; }
        public required string FullName { get; set; }
        public string IdentificationDocument { get; set; }
        public DateTime BirthDate { get; set; }
        public string Email { get; set; }
        public string TelephoneNumber { get; set; }
    }
}
