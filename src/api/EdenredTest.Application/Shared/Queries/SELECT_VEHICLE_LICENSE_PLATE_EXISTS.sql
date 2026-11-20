SELECT EXISTS (
    SELECT 1
    FROM Vehicles
    WHERE license_plate = @LicensePlate
      AND (@ExcludedVehicleId IS NULL OR id <> @ExcludedVehicleId)
);
