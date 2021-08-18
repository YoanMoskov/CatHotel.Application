namespace CatHotel.Test.Data
{
    using System;
    using CatHotel.Data.Models;
    using CatHotel.Data.Models.Enums;
    using static Cats;

    public static class Reservations
    {
        public static RoomType TestRoom
            => new()
            {
                Id = 1,
                Name = "TestRoomName",
                Description = "asd",
                PricePerDay = 12m
            };

        public static Reservation TestReservation
            => new()
            {
                Id = "1",
                DateOfReservation = DateTime.UtcNow,
                Arrival = DateTime.UtcNow.AddDays(1),
                Departure = DateTime.UtcNow.AddDays(2),
                UserId = "TestId"
            };

        public static Reservation TestActiveReservation
            => new()
            {
                Id = "1",
                DateOfReservation = DateTime.UtcNow,
                Arrival = DateTime.UtcNow.AddDays(-2),
                Departure = DateTime.UtcNow.AddDays(2),
                UserId = "TestId",
                ReservationState = ReservationState.Active,
                IsApproved = true,
                Payment = new Payment
                {
                    TotalPrice = 0
                },
                RoomType = TestRoom
            };

        public static Reservation TestActiveFromPendingReservation
            => new()
            {
                Id = "1",
                DateOfReservation = DateTime.UtcNow,
                Arrival = DateTime.UtcNow.AddDays(-1),
                Departure = DateTime.UtcNow.AddDays(2),
                UserId = "TestId",
                ReservationState = ReservationState.Pending,
                IsApproved = true,
                Payment = new Payment
                {
                    TotalPrice = 0
                },
                RoomType = TestRoom
            };

        public static Reservation TestExpiredFromActiveReservation
            => new()
            {
                Id = "1",
                DateOfReservation = DateTime.UtcNow,
                Arrival = DateTime.UtcNow.AddDays(-10),
                Departure = DateTime.UtcNow.AddDays(-8),
                UserId = "TestId",
                ReservationState = ReservationState.Active,
                IsApproved = true,
                Payment = new Payment
                {
                    TotalPrice = 0
                },
                RoomType = TestRoom
            };

        public static Reservation TestExpiredReservation
            => new()
            {
                Id = "1",
                DateOfReservation = DateTime.UtcNow,
                Arrival = DateTime.UtcNow.AddDays(-10),
                Departure = DateTime.UtcNow.AddDays(-8),
                UserId = "TestId",
                ReservationState = ReservationState.Expired,
                IsApproved = true,
                Payment = new Payment
                {
                    TotalPrice = 0
                },
                RoomType = TestRoom
            };

        public static Reservation TestPendingReservation
            => new()
            {
                Id = "1",
                DateOfReservation = DateTime.UtcNow,
                Arrival = DateTime.UtcNow.AddDays(8),
                Departure = DateTime.UtcNow.AddDays(10),
                UserId = "TestId",
                ReservationState = ReservationState.Pending,
                IsApproved = true,
                Payment = new Payment
                {
                    TotalPrice = 0
                },
                RoomType = TestRoom
            };

        public static Reservation TestApprovedReservation
            => new()
            {
                Id = "1",
                DateOfReservation = DateTime.UtcNow,
                Arrival = DateTime.UtcNow.AddDays(8),
                Departure = DateTime.UtcNow.AddDays(10),
                UserId = "TestId",
                ReservationState = ReservationState.Pending,
                IsApproved = true,
                Payment = new Payment
                {
                    TotalPrice = 0
                },
                RoomType = TestRoom
            };

        public static Reservation TestPendingApprovalReservation
            => new()
            {
                Id = "1",
                DateOfReservation = DateTime.UtcNow,
                Arrival = DateTime.UtcNow.AddDays(1),
                Departure = DateTime.UtcNow.AddDays(2),
                UserId = "TestId",
                ReservationState = ReservationState.Pending,
                IsApproved = false,
                Payment = new Payment
                {
                    TotalPrice = 0
                },
                RoomType = TestRoom
            };

        public static CatReservation TestCatReservation
            => new()
            {
                CatId = TestCats[0].Id,
                ReservationId = TestReservation.Id
            };

        public static CatReservation[] TestCatReservations
            => new[]
            {
                new()
                {
                    CatId = TestCats[0].Id,
                    ReservationId = TestReservation.Id
                },

                new CatReservation
                {
                    CatId = TestCats[1].Id,
                    ReservationId = TestReservation.Id
                }
            };
    }
}