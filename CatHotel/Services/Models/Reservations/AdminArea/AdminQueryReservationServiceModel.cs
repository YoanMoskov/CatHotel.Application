namespace CatHotel.Services.Models.Reservations.AdminArea
{
    using System.Collections.Generic;
    using CommonArea;

    public class AdminQueryReservationServiceModel
    {
        public int CurrentPage { get; set; }

        public int ResPerPage { get; set; }

        public int TotalReservations { get; set; }

        public IEnumerable<ResServiceModel> Reservations { get; set; }
    }
}