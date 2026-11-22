SELECT
    v.id AS "Id",
    v.client_id AS "ClientId",
    c.full_name AS "ClientName",
    v.license_plate AS "LicensePlate",
    v.manufacture_year AS "ManufactureYear",
    v.make AS "Brand",
    v.model AS "Model"
FROM Vehicles v
LEFT JOIN Clients c ON c.id = v.client_id
WHERE v.id = @Id;
