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
    client_id BIGINT REFERENCES clients(id),
    license_plate VARCHAR(10),
    manufacture_year INTEGER,
    make VARCHAR(100),
    model VARCHAR(100)
);

INSERT INTO public.clients (full_name, identification_document, birth_date, email, phone)
VALUES (
    'Pedro Arruda',
    '41256211877',
    '1993-01-06',
    'pedro.arruda@outlook.com.br',
    '11941622236'
);