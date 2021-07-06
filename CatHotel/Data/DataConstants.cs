namespace CatHotel.Data
{
    using System.Text.RegularExpressions;

    public static class DataConstants
    {
        public static class User
        {
            public const int MinNameLength = 3;
            public const int MaxNameLength = 50;

            public const string EmailRegex = @"(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*|""(?:[\x01-\x08\x0b\x0c\x0e-\x1f\x21\x23-\x5b\x5d-\x7f]|\\[\x01-\x09\x0b\x0c\x0e-\x7f])*"")@(?:(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?|\[(?:(?:(2(5[0-5]|[0-4][0-9])|1[0-9][0-9]|[1-9]?[0-9]))\.){3}(?:(2(5[0-5]|[0-4][0-9])|1[0-9][0-9]|[1-9]?[0-9])|[a-z0-9-]*[a-z0-9]:(?:[\x01-\x08\x0b\x0c\x0e-\x1f\x21-\x5a\x53-\x7f]|\\[\x01-\x09\x0b\x0c\x0e-\x7f])+)\])";
            public const int MaxEmailLength = 100;

            public const int MinUsernameLength = 5;
            public const int MaxUsernameLength = 50;

            public const int MinPhoneNumberLength = 5;
            public const int MaxPhoneNumberLength = 15;

        }

        public static class Address
        {
            public const int CountryMaxLength = 50;
            public const int CityMaxLength = 50;
            public const int FullAddressMaxLength = 150;
        }

        public static class Employee
        {
            public const int MaxNameLength = 50;
            public const int MaxPhoneNumberLength = 15;
            public const int MaxEmailLength = 100;
        }

        public static class Cat
        {
            public const int MinNameLength = 2;
            public const int MaxNameLength = 50;
            public const int MinAge = 1;
            public const int MaxAge = 15;
        }

        public static class Breed
        {
            public const int MaxBreedLength = 50;
        }

        public static class Room
        {
            public const int MaxTypeNameLength = 50;
        }
    }
}