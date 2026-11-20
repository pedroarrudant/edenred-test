SELECT
    id AS "Id",
    license_plate AS "LicensePlate",
    manufacture_year AS "ManufactureYear",
    make AS "Brand",
    model AS "Model"
FROM Vehicles
ORDER BY id
LIMIT 100;
