namespace CatHotel.Data
{
    public static class DataConstants
    {
        public static class User
        {
            public const int MinNameLength = 3;
            public const int MaxNameLength = 50;

            public const string EmailRegex =
                @"(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*|""(?:[\x01-\x08\x0b\x0c\x0e-\x1f\x21\x23-\x5b\x5d-\x7f]|\\[\x01-\x09\x0b\x0c\x0e-\x7f])*"")@(?:(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?|\[(?:(?:(2(5[0-5]|[0-4][0-9])|1[0-9][0-9]|[1-9]?[0-9]))\.){3}(?:(2(5[0-5]|[0-4][0-9])|1[0-9][0-9]|[1-9]?[0-9])|[a-z0-9-]*[a-z0-9]:(?:[\x01-\x08\x0b\x0c\x0e-\x1f\x21-\x5a\x53-\x7f]|\\[\x01-\x09\x0b\x0c\x0e-\x7f])+)\])";

            public const int MaxEmailLength = 100;

            public const int MinUsernameLength = 5;
            public const int MaxUsernameLength = 50;

            public const int MinPhoneNumberLength = 5;
            public const int MaxPhoneNumberLength = 15;
        }

        public static class Cat
        {
            public const int MinNameLength = 2;
            public const int MaxNameLength = 50;
            public const int MinAge = 1;
            public const int MaxAge = 15;
            public const int MinAdditionalInformation = 10;
        }

        public static class Breed
        {
            public const int MaxBreedLength = 50;
        }

        public static class RoomType
        {
            public const int MaxTypeNameLength = 50;
        }

        public static class Grooming
        {
            public const int MinDescribePreferredStyle = 10;
        }

        public static class Style
        {
            public const string NaturalStyleName = "Natural";
            public const string NaturalStyleDesc =
                "This keeps your long hair cat's coat natural without cutting into their coat & colors.This is the secret to caring for your cat's coat. This will maintain your cat's natural beauty & remove all unwanted oil, trapped dander & dead fur from building up in their coat. A belly shave can remove even more fur and eliminate problem areas without being visible.";
            public const decimal NaturalStylePrice = 65m;
            public const string NaturalPhotoUrl = "https://www.themainlion.com/s/cc_images/cache_4106884985.jpg";

            public const string TigonStyleName = "Tigon";
            public const string TigonStyleDesc =
                "This style is 1/2 Tiger & 1/2 Lion. This style leaves a Tiger head on a Lion pattern body. There is no mane and the head is purrfectly manicured round. The body is styled as a Lion Cut with \"boots\" left on the legs & a Lion tail. Purrfect for short haired cats too.";
            public const decimal TigonStylePrice = 70m;
            public const string TigonPhotoUrl = "https://www.themainlion.com/s/cc_images/cache_4106884983.jpg";

            public const string LionStyleName = "The Main Lion";
            public const string LionStyleDesc =
                "We refer to the standard Lion Cut as \"The Main Lion\". \"The Main Lion\" leaves a full mane, a plume on the end of the tail & furry boots. The body is shaved. Optional tail styles available. All Lion Cuts are fully customizable.";
            public const decimal LionStylePrice = 85m;
            public const string LionPhotoUrl = "https://www.themainlion.com/s/cc_images/cache_4106884984.jpg";
        }
    }
}