namespace CatHotel.Models.Reservation.ViewModels
{
    using System;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Data.Models.Enums;
    using Services.Models.Reservations;
    using Services.Models.Reservations.CommonArea;

    public class ResViewModel
    {
        public string Id { get; init; }

        public DateTime DateOfReservation { get; init; }

        [BindProperty, DataType(DataType.Date)]
        public string Arrival { get; init; }

        [BindProperty, DataType(DataType.Date)]
        public string Departure { get; init; }

        public IEnumerable<ResCatServiceModel> Cats { get; set; }

        public string RoomTypeName { get; init; }

        public string TotalPrice { get; init; }

        public ReservationState ReservationState { get; set; }

        public bool IsApproved { get; set; }
    }
}