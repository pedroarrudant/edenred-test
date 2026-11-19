namespace EdenredTest.Controllers.Requests
{
    public class InsertClientRequest
    {
        public string FullName { get; set; } = null!;
        public string IdentificationDocument { get; set; } = null!;
        public DateTime BirthDate { get; set; }
        public string Email { get; set; } = null!;
        public string TelephoneNumber { get; set; } = null!;
    }
}
