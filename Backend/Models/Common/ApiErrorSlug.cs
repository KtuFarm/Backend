namespace Backend.Models.Common
{
    public static class ApiErrorSlug
    {
        public const string InvalidHeaders = "invalid_headers";
        public const string InvalidDto = "invalid_body";
        public const string EmptyParameter = "parameter_empty";
        public const string InvalidNumber = "invalid_numeric_value";
        public const string InvalidPharmaceuticalForm = "invalid_pharmaceutical_form";
        public const string InvalidPercentage = "invalid_percentage";
        public const string InvalidBarcode = "invalid_barcode";
        public const string InvalidPassword = "invalid_password";
        public const string InvalidEmployeeState = "invalid_employee_state";
        public const string ResourceNotFound = "object_not_found";
        public const string InvalidDateSpan = "invalid_date_span";
        public const string Unauthorized = "user_not_authorized";
        public const string ObjectAlreadyExists = "object_already_exists";
        public const string InvalidStatus = "invalid_status";
        public const string InsufficientBalance = "insufficient_balance";
    }
}
