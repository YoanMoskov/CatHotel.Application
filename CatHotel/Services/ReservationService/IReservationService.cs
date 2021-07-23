namespace CatHotel.Services.ReservationService
{
    using System.Collections.Generic;
    using CatHotel.Models.Reservation.FormModels;
    using CatHotel.Models.Reservation.ViewModels;
    using CatHotel.Models.RoomType;
    using Microsoft.AspNetCore.Mvc.Rendering;

    public interface IReservationService
    {
        void CreateReservation(ResFormModel res, string userId);

        IEnumerable<SelectListItem> GetCatsSelectList(string userId);

        IEnumerable<ResRoomTypeViewModel> GetRoomTypes();

        IEnumerable<ResCatViewModel> GetCatsInReservations(string resId);

        IEnumerable<ResViewModel> GetReservations(string userId);

        void FilterReservations(IEnumerable<ResViewModel> reservations);
    }
}