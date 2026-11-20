using System;
using System.Collections.Generic;
using System.Text;

namespace EdenredTest.Application.Shared.Constants
{
    public static class Messages
    {
        public static class Error
        {
            public static string InvalidEmail { get; set; } = "Invalid E-mail for user {0}";
            public static string IdentificationDocumentAlreadyInUse { get; set; } = "Indetification document already in use for user {0}";
            public const string LicensePlateAlreadyInUse = "License plate is already in use.";
            public const string InvalidManufactureYear = "Manufacture year must be greater than the current year.";
            public static string InvalidName { get; set; } = "Invalid name is informed";
        }
    }
}
