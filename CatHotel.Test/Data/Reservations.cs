namespace CatHotel.Test.Data
{
    using System;
    using CatHotel.Data.Models;

    using static Cats;

    public static class Reservations
    {
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