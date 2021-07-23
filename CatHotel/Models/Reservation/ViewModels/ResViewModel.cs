namespace CatHotel.Models.Reservation.ViewModels
{
    using System;
    using Microsoft.AspNetCore.Mvc;
    using RoomType;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class ResViewModel
    {
        public string Id { get; set; }

        public DateTime DateOfReservation { get; set; }

        [BindProperty, DataType(DataType.Date)]
        public string Arrival { get; set; }

        [BindProperty, DataType(DataType.Date)]
        public string Departure { get; set; }

        public IEnumerable<ResCatViewModel> Cats { get; set; }

        public ResRoomTypeViewModel RoomType { get; set; }

        public ResPaymentViewModel Payment { get; set; }

        public bool IsActive { get; set; }
    }
}