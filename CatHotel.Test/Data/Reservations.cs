namespace CatHotel.Test.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using CatHotel.Data.Models;
    using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
    using Microsoft.VisualStudio.TestPlatform.ObjectModel.DataCollection;

    public static class Reservations
    {
        public static Cat TestCat
            => new Cat
            {
                Id = "1",
                Name = "test",
                Age = 2,
                PhotoUrl = "",
                DateAdded = DateTime.UtcNow,
                BreedId = 1,
                Breed = new Breed()
                {
                    Id = 1,
                    Name = "Test"
                },
                UserId = "TestId"
            };

        public static RoomType TestRoom
            => new RoomType
            {
                Id = 1,
                Name = "TestRoomName",
                Description = "asd",
                PricePerDay = 12m
            };

        public static Reservation TestReservation
            => new Reservation()
            {
                Id = "1",
                DateOfReservation = DateTime.UtcNow,
                Arrival = DateTime.UtcNow.AddDays(1),
                Departure = DateTime.UtcNow.AddDays(2),
                UserId = "TestId"
            };

        public static CatReservation TestCatReservation
            => new CatReservation()
            {
                CatId = TestCat.Id,
                ReservationId = TestReservation.Id
            };
    }
}