namespace EdenredTest.Controllers.Requests
{
    public class InsertVehicleRequest
    {
        public long? ClientId { get; set; }
        public string LicensePlate { get; set; } = null!;
        public int ManufactureYear { get; set; }
        public string Brand { get; set; } = null!;
        public string Model { get; set; } = null!;
    }
}
