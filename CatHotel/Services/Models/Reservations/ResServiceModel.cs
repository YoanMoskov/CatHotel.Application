namespace CatHotel.Services.Models.Reservations
{
    using System;
    using System.Collections.Generic;

    public class ResServiceModel
    {
        public string Id { get; init; }

        public DateTime DateOfReservation { get; init; }

        public string Arrival { get; init; }

        public string Departure { get; init; }

        public string RoomTypeName { get; init; }

        public string TotalPrice { get; init; }

        public bool IsActive { get; set; }

        public IEnumerable<ResCatServiceModel> Cats { get; set; }
    }
}