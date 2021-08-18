namespace CatHotel.Services.Models.Groomings.CommonArea
{
    using System;

    public class GroomingServiceModel
    {
        public string Id { get; set; }

        public string CatName { get; set; }

        public string BreedName { get; set; }

        public string StyleName { get; set; }

        public string Appointment { get; set; }

        public bool IsApproved { get; set; }
    }
}