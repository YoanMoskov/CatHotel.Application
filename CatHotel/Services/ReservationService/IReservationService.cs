namespace CatHotel.Services.ReservationService
{
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Models.Reservations;
    using System;
    using System.Collections.Generic;

    public interface IReservationService
    {
        void Create(
            DateTime arrival,
            DateTime departure,
            int roomTypeId,
            string[] catIds,
            string userId);

        IEnumerable<SelectListItem> CatsSelectList(string userId);

        IEnumerable<ResRoomTypeServiceModel> RoomTypes();

        IEnumerable<ResCatServiceModel> CatsInReservations(string resId);

        IEnumerable<ResServiceModel> All(string userId);
    }
}