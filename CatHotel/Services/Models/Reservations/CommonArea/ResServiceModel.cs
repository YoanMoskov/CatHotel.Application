namespace CatHotel.Services.Models.Reservations.CommonArea
{
    using System;
    using System.Collections.Generic;
    using Data.Models.Enums;

    public class ResServiceModel
    {
        public string Id { get; init; }

        public DateTime DateOfReservation { get; init; }

        public string Arrival { get; init; }

        public string Departure { get; init; }

        public string RoomTypeName { get; init; }

        public string TotalPrice { get; init; }

        public ReservationState ReservationState { get; set; }

        public bool IsApproved { get; init; }

        public IEnumerable<ResCatServiceModel> Cats { get; set; }
    }
}