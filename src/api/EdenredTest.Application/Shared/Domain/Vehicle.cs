namespace EdenredTest.Application.Shared.Domain
{
    public class Vehicle
    {
        public long Id { get; set; }
        public long? ClientId { get; set; }
        public string? ClientName { get; set; }
        public string LicensePlate { get; set; }
        public int ManufactureYear { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
    }
}
