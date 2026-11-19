INSERT INTO Clients (
    full_name,
    identification_document,
    birth_date,
    email,
    phone
)
VALUES (
    @FullName,
    @IdentificationDocument,
    @BirthDate,
    @Email,
    @Phone
);
