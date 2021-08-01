﻿namespace CatHotel.Models.Reservation.ViewModels
{
    using System;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Services.Models.Reservations;

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

        public bool IsActive { get; set; }
    }
}