namespace CatHotel.Services.ReservationServices
{
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Models.Reservation.FormModels;
    using Models.Reservation.ViewModels;
    using Models.RoomType;

    public interface IReservationService
    {
        void CreateReservation(ReservationFormModel res, string userId);

        IEnumerable<SelectListItem> GetCatsSelectList(string userId);

        IEnumerable<RoomTypeViewModel> GetRoomTypes();

        IEnumerable<ResCatViewModel> GetCatsInReservations(string resId);

        IEnumerable<ReservationViewModel> GetReservations(string userId);
    }
}