namespace CatHotel.Services.ReservationService
{
    using System;
    using System.Collections.Generic;
    using Areas.Admin.Models.Enums.Reservations;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Models.Reservations.AdminArea;
    using Models.Reservations.CommonArea;

    public interface IReservationService
    {
        void Create(
            DateTime arrival,
            DateTime departure,
            int roomTypeId,
            string[] catIds,
            string userId);

        IEnumerable<ResServiceModel> All(string userId);

        AdminQueryReservationServiceModel AdminAll(
            string roomName = null,
            int currentPage = 1,
            ResSorting sorting = ResSorting.Newest,
            ResFiltering filtering = ResFiltering.Pending,
            int resPerPage = int.MaxValue);

        bool AdminApprove(string resId);

        bool DoesCatExist(string catId);

        string AreCatsInResTimeFrame(string[] catIds, DateTime arrival, DateTime departure);

        bool DoesRoomTypeExist(int roomTypeId);

        IEnumerable<SelectListItem> CatsSelectList(string userId);

        IEnumerable<ResRoomTypeServiceModel> RoomTypes();
    }
}