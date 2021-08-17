namespace CatHotel.Models.Grooming.FormModel
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Infrastructure;
    using Microsoft.AspNetCore.Mvc;
    using ViewModel;

    public class AddGroomingModel
    {
        public GroomingStyleModel Style { get; set; }

        public GroomingCatViewModel Cat { get; set; }

        [Required]
        [Appointment]
        [BindProperty, DataType(DataType.Date)]
        public DateTime Appointment { get; set; } = DateTime.UtcNow;
    }
}