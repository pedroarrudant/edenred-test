using EdenredTest.Application.Shared.Domain;

namespace EdenredTestTests.Static
{
    public static class VehicleStatic
    {
        public static Vehicle Create() => new Vehicle()
        {
            Id = 1,
            LicensePlate = "ABC1D23",
            ManufactureYear = 2020,
            Brand = "Toyota",
            Model = "Corolla"
        };
    }
}
