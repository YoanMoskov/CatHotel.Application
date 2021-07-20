namespace CatHotel.Models.Reservation.ViewModels
{
    using FormModels;
    using RoomType;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Microsoft.AspNetCore.Mvc;

    public class ResViewModel
    {
        public string Id { get; set; }

        [BindProperty, DataType(DataType.Date)]
        public string Arrival { get; set; }

        [BindProperty, DataType(DataType.Date)]
        public string Departure { get; set; }

        public IEnumerable<ResCatViewModel> Cats { get; set; }

        public int RoomTypeId { get; set; }

        public IEnumerable<RoomTypeViewModel> RoomTypes { get; set; }

        public bool IsActive { get; set; }
    }
}