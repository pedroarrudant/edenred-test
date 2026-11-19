UPDATE Clients
SET full_name = @FullName,
    identification_document = @IdentificationDocument,
    birth_date = @BirthDate,
    email = @Email,
    phone = @Phone
WHERE id = @Id;
