namespace CatHotel.Services.ReservationService
{
    using System;
    using System.Collections.Generic;
    using CatHotel.Models.Reservation.FormModels;
    using CatHotel.Models.Reservation.ViewModels;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Models.Reservations;

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

        void FilterReservations();
    }
}