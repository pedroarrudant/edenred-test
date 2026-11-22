INSERT INTO Vehicles (client_id, license_plate, manufacture_year, make, model)
SELECT id, @LicensePlate, @ManufactureYear, @Brand, @Model
FROM Clients
WHERE id = @ClientId
FOR KEY SHARE;
