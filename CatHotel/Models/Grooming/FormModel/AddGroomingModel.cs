namespace CatHotel.Models.Grooming.FormModel
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Infrastructure.Extensions;
    using Microsoft.AspNetCore.Mvc;
    using Services.Models.Groomings.CommonArea;

    public class AddGroomingModel
    {
        public GroomingStyleServiceModel Style { get; set; }

        public GroomingCatServiceModel Cat { get; set; }

        [Required]
        [Appointment]
        [BindProperty]
        [DataType(DataType.Date)]
        public DateTime Appointment { get; set; } = DateTime.UtcNow;
    }
}