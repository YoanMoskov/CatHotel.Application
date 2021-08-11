namespace CatHotel.Areas.Admin.Models.Reservations
{
    using Enums.Reservations;
    using Services.Models.Cats.AdminArea;
    using Services.Models.Reservations.CommonArea;
    using System.Collections.Generic;
    using CatHotel.Models.Reservation.ViewModels;

    public class AdminAllReservationsQueryModel
    {
        public const int ResPerPage = 6;

        public string RoomType { get; set; }

        public int CurrentPage { get; set; } = 1;

        public int TotalReservations { get; set; }

        public ResSorting Sorting { get; set; }

        public ResFiltering Filtering { get; set; }

        public IEnumerable<ResRoomTypeServiceModel> RoomTypes { get; set; }

        public IEnumerable<ResServiceModel> Reservations { get; set; }
    }
}