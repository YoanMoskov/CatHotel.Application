namespace CatHotel.Test.Data
{
    using System;
    using CatHotel.Data.Models;
    using static Cats;

    public static class Groomings
    {
        public static Style TestStyle
            => new()
            {
                Id = 1,
                Name = "TestStyle",
                Description = "TestDescription",
                PhotoUrl = "TestPhotoUrl",
                Price = 10m
            };

        public static Style TestStyle2
            => new()
            {
                Id = 2,
                Name = "TestStyle",
                Description = "TestDescription",
                PhotoUrl = "TestPhotoUrl",
                Price = 10m
            };

        public static Grooming TestGrooming
            => new()
            {
                Id = "TestGroomId",
                UserId = "TestId",
                CatId = TestCat.Id,
                Cat = TestCat,
                StyleId = TestStyle.Id,
                Style = TestStyle,
                DateOfCreation = DateTime.UtcNow.AddDays(-5),
                Appointment = DateTime.UtcNow.AddDays(2),
                IsApproved = false,
                IsExpired = false
            };

        public static Grooming TestApprovedGrooming
            => new()
            {
                Id = "TestGroomId",
                UserId = "TestId",
                CatId = TestCat.Id,
                Cat = TestCat,
                StyleId = TestStyle.Id,
                Style = TestStyle,
                DateOfCreation = DateTime.UtcNow.AddDays(-5),
                Appointment = DateTime.UtcNow.AddDays(2),
                IsApproved = true,
                IsExpired = false
            };

        public static Grooming TestMoveToExpiredGrooming
            => new()
            {
                Id = "TestGroomId2",
                UserId = "TestId",
                CatId = TestCat2.Id,
                Cat = TestCat2,
                StyleId = TestStyle2.Id,
                Style = TestStyle2,
                DateOfCreation = DateTime.UtcNow.AddDays(-10),
                Appointment = DateTime.UtcNow.AddDays(-2),
                IsApproved = true,
                IsExpired = false
            };
    }
}