UPDATE Vehicles
SET license_plate = @LicensePlate,
    manufacture_year = @ManufactureYear,
    make = @Brand,
    model = @Model
WHERE id = @Id;
