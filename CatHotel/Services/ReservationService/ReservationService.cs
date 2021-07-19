namespace CatHotel.Services.ReservationService
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using Data;
    using Data.Models;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Models.Reservation.FormModels;
    using Models.Reservation.ViewModels;
    using Models.RoomType;
    using ReservationServices;
    using UserService;

    public class ReservationService : IReservationService
    {
        private readonly ApplicationDbContext data;
        private readonly IUserService userService;

        public ReservationService(ApplicationDbContext data)
        {
            this.data = data;
        }

        public void CreateReservation(ReservationFormModel res, string userId)
        {
            var newReservation = new Reservation()
            {
                DateOfReservation = DateTime.UtcNow.ToLocalTime(),
                Arrival = res.Arrival,
                Departure = res.Departure,
                RoomTypeId = res.RoomTypeId,
                UserId = userId
            };

            foreach (var catId in res.CatIds)
            {
                var cat = data.Cats.FirstOrDefault(c => c.Id == catId);
                newReservation.Cats.Add(cat);
            }

            data.Reservations.Add(newReservation);
            data.SaveChanges();
        }

        public IEnumerable<SelectListItem> GetCatsSelectList(string userId)
            => this.data
                .Cats
                .Where(c => c.UserId == userId)
                .Select(c => new SelectListItem()
                {
                    Value = c.Id,
                    Text = $"{c.Name} - {c.Breed.Name}"
                })
                .ToList();

        public IEnumerable<RoomTypeViewModel> GetRoomTypes()
            => this.data
                .RoomTypes
                .Select(rt => new RoomTypeViewModel()
                {
                    Id = rt.Id,
                    Name = rt.Name
                })
                .ToList();

        public IEnumerable<ResCatViewModel> GetCatsInReservations(string resId)
            => data.Cats
                .Where(c => c.ReservationId == resId)
                .Select(c => new ResCatViewModel()
                {
                    Name = c.Name,
                    BreedName = c.Breed.Name
                })
                .ToList();

        public IEnumerable<ReservationViewModel> GetReservations(string userId)
        {
            var resevations = data.Reservations
                .Where(r => r.UserId == userId)
                .Select(r => new ReservationViewModel()
                {
                    Id = r.ReservationId,
                    Arrival = r.Arrival.ToShortDateString(),
                    Departure = r.Departure.ToShortDateString(),
                    RoomTypeId = r.RoomTypeId
                })
                .ToList();

            foreach (var res in resevations)
            {
                res.Cats = GetCatsInReservations(res.Id);
            }

            return resevations;
        }
    }
}