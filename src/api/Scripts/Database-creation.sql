CREATE TABLE Clients (
    id BIGINT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    full_name VARCHAR(200),
    identification_document VARCHAR(11),
    birth_date DATE,
    email VARCHAR(254),
    phone VARCHAR(30)
);

CREATE TABLE Vehicles (
    id BIGINT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    client_id BIGINT NOT NULL REFERENCES clients(id),
    license_plate VARCHAR(10),
    manufacture_year INTEGER,
    make VARCHAR(100),
    model VARCHAR(100)
);

WITH mock_client AS (
    INSERT INTO public.clients (full_name, identification_document, birth_date, email, phone)
    VALUES ('Marina Exemplo', '00000000000', DATE '1990-01-01', 'marina@example.com', '11000000000')
    RETURNING id
)
INSERT INTO public.vehicles (client_id, license_plate, manufacture_year, make, model)
SELECT id, 'TST0A00', 2022, 'Fiat', 'Argo'
FROM mock_client;
