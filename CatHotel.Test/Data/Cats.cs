namespace CatHotel.Test.Data
{
    using System;
    using CatHotel.Data.Models;

    public static class Cats
    {
        public static Cat TestCat
            => new Cat
            {
                Id = "1",
                Name = "test",
                Age = 2,
                PhotoUrl = "https://ichef.bbci.co.uk/news/976/cpsprodpb/A7E9/production/_118158924_gettyimages-507245091.jpg",
                DateAdded = DateTime.UtcNow,
                BreedId = 1,
                Breed = new Breed()
                {
                    Id = 1,
                    Name = "TestBreed"
                },
                UserId = "TestId"
            };

        public static Breed TestBreed
            => new Breed()
            {
                Id = 1,
                Name = "TestBreed"
            };

        public static Breed TestBreed2
            => new Breed()
            {
                Id = 2,
                Name = "TestBreed2"
            };
    }
}