SELECT
    id AS "Id",
    full_name AS "FullName",
    identification_document AS "IdentificationDocument",
    CAST(birth_date AS timestamp without time zone) AS "BirthDate",
    email AS "Email",
    phone AS "TelephoneNumber"
FROM Clients
ORDER BY id
LIMIT 100;
