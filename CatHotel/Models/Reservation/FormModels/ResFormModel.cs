﻿namespace CatHotel.Models.Reservation.FormModels
{
    using Infrastructure;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Services.Models.Reservations;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Services.Models.Reservations.CommonArea;

    public class ResFormModel
    {
        [Arrival]
        [Required]
        [BindProperty, DataType(DataType.Date)]
        public DateTime Arrival { get; set; } = DateTime.UtcNow.ToLocalTime();

        [Required]
        [Departure]
        [BindProperty, DataType(DataType.Date)]
        public DateTime Departure { get; set; } = DateTime.UtcNow.ToLocalTime();

        [CatIds]
        public string[] CatIds { get; set; }

        public IEnumerable<SelectListItem> Cats { get; set; }

        public int RoomTypeId { get; set; }

        public IEnumerable<ResRoomTypeServiceModel> RoomTypes { get; set; }

        public bool IsActive { get; set; }
    }
}