namespace CatHotel.Services.ReservationServices
{
    using System.Collections.Generic;
    using Models.RoomType;
    using Models.Reservation.FormModels;
    using Models.Reservation.ViewModels;
    using Microsoft.AspNetCore.Mvc.Rendering;

    public interface IReservationService
    {
        void CreateReservation(ResFormModel res, string userId);

        IEnumerable<SelectListItem> GetCatsSelectList(string userId);

        IEnumerable<RoomTypeViewModel> GetRoomTypes();

        IEnumerable<ResCatViewModel> GetCatsInReservations(string resId);

        IEnumerable<ResViewModel> GetReservations(string userId);
    }
}