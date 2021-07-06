namespace CatHotel.Data
{
    public static class ErrorMessageConstants
    {
        public static class ClientErrorMessages
        {
            public const string FullNameError = "First Name should be between 3 and 50 characters long.";
            public const string UsernameError = "Username should be between 5 and 30 characters long.";
            public const string EmailError = "The email you entered is invalid.";
            public const string PhoneNumberError = "The phone number you entered is invalid it should be between 5 and 15 digits.";
        }
    }
}