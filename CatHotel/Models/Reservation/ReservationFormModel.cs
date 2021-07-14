namespace CatHotel.Models.Reservation
{
    using System;
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using RoomType;

    public class ReservationFormModel
    {
        public DateTime DateOfReservation { get; set; }

        public DateTime Arrival { get; set; }

        public DateTime Departure { get; set; }

        public string[] CatIds { get; set; }

        public IEnumerable<SelectListItem> Cats { get; set; }

        public int RoomTypeId { get; set; }

        public IEnumerable<RoomTypeViewModel> RoomTypes { get; set; }
    }
}