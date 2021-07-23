namespace CatHotel.Models.Reservation.FormModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Infrastructure;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using RoomType;

    public class ResFormModel
    {
        [Required]
        [Arrival]
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

        public IEnumerable<ResRoomTypeViewModel> RoomTypes { get; set; }

        public bool IsActive { get; set; }
    }
}