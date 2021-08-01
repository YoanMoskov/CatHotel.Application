namespace CatHotel.Services.ReservationService
{
    using CatHotel.Models.Reservation.ViewModels;
    using Data;
    using Data.Models;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Models.Reservations;
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;

    public class ReservationService : IReservationService
    {
        private readonly ApplicationDbContext _data;

        public ReservationService(ApplicationDbContext data)
        {
            this._data = data;
        }

        public void Create(DateTime arrival, DateTime departure, int roomTypeId, string[] catIds, string userId)
        {
            var price = 0m;
            var room = _data.RoomTypes
                .FirstOrDefault(r => r.Id == roomTypeId);

            if (room != null)
            {
                price = room.PricePerDay * (departure - arrival).Days * catIds.Count();
            }

            var newReservation = new Reservation()
            {
                DateOfReservation = DateTime.UtcNow,
                Arrival = arrival,
                Departure = departure,
                RoomTypeId = roomTypeId,
                UserId = userId,
                Payment = new Payment()
                {
                    TotalPrice = price,
                    isPaid = false,
                },
                IsActive = true
            };

            var allCatsReservation = catIds.Select(catId
                => new CatReservation() {CatId = catId, Reservation = newReservation}).ToList();

            _data.CatsReservations.AddRange(allCatsReservation);
            _data.SaveChanges();
        }

        public IEnumerable<SelectListItem> CatsSelectList(string userId)
            => this._data
                .Cats
                .Where(c => c.UserId == userId && c.IsDeleted == false)
                .Select(c => new SelectListItem()
                {
                    Value = c.Id,
                    Text = $"{c.Name} - {c.Breed.Name}"
                })
                .ToList();

        public IEnumerable<ResRoomTypeServiceModel> RoomTypes()
            => this._data
                .RoomTypes
                .Select(rt => new ResRoomTypeServiceModel()
                {
                    Id = rt.Id,
                    Name = rt.Name
                })
                .ToList();

        public IEnumerable<ResCatServiceModel> CatsInReservations(string resId)
            => _data
                .CatsReservations
                .Where(cr => cr.ReservationId == resId)
                .Select(cr => new ResCatServiceModel()
                {
                    Name = cr.Cat.Name,
                    BreedName = cr.Cat.Breed.Name
                })
                .ToList();

        public IEnumerable<ResServiceModel> All(string userId)
        {
            FilterReservations();

            var reservations = _data
                .Reservations
                .Where(r => r.UserId == userId && r.IsActive)
                .Select(r => new ResServiceModel()
                {
                    Id = r.Id,
                    DateOfReservation = r.DateOfReservation,
                    Arrival = r.Arrival.ToString("MM/dd/yyyy"),
                    Departure = r.Departure.ToString("MM/dd/yyyy"),
                    RoomTypeName = r.RoomType.Name,
                    TotalPrice = $"$ {r.Payment.TotalPrice:f2}",
                    IsActive = r.IsActive
                })
                .OrderByDescending(r => r.DateOfReservation)
                .ToList();

            foreach (var res in reservations)
            {
                res.Cats = CatsInReservations(res.Id);
            }

            return reservations;
        }

        public void FilterReservations()
        {
            var reservations = _data
                .Reservations
                .Where(r => r.IsActive == true)
                .ToList();

            foreach (var res in reservations)
            {
                if (res.Departure < DateTime.UtcNow)
                {
                    res.IsActive = false;
                    _data.SaveChanges();
                }
            }
        }

        private DateTime ConvertToDateTime(string dateString)
            => DateTime.ParseExact(dateString, "MM/dd/yyyy", CultureInfo.InvariantCulture);
    }
}