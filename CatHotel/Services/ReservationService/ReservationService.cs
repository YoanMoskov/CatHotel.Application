namespace CatHotel.Services.ReservationService
{
    using Data;
    using Data.Models;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Models.Reservation.FormModels;
    using Models.Reservation.ViewModels;
    using Models.RoomType;
    using ReservationServices;
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;

    public class ReservationService : IReservationService
    {
        private readonly ApplicationDbContext data;

        public ReservationService(ApplicationDbContext data)
        {
            this.data = data;
        }

        public void CreateReservation(ResFormModel res, string userId)
        {
            var allCatsReservation = new List<CatReservation>();
            var price = 0m;
            var room = data.RoomTypes
                .FirstOrDefault(r => r.Id == res.RoomTypeId);

            if (room != null)
            {
                price = room.PricePerDay * (res.Departure - res.Arrival).Days * res.CatIds.Count();
            }

            var newReservation = new Reservation()
            {
                DateOfReservation = DateTime.UtcNow,
                Arrival = res.Arrival,
                Departure = res.Departure,
                RoomTypeId = res.RoomTypeId,
                UserId = userId,
                Payment = new Payment()
                {
                    TotalPrice = price,
                    isPaid = false,
                },
                IsActive = true
            };

            foreach (var catId in res.CatIds)
            {
                var newCatReservation = new CatReservation()
                {
                    CatId = catId,
                    Reservation = newReservation
                };
                allCatsReservation.Add(newCatReservation);
            }

            data.CatsReservations.AddRange(allCatsReservation);
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

        public IEnumerable<ResRoomTypeViewModel> GetRoomTypes()
            => this.data
                .RoomTypes
                .Select(rt => new ResRoomTypeViewModel()
                {
                    Id = rt.Id,
                    Name = rt.Name
                })
                .ToList();

        public IEnumerable<ResCatViewModel> GetCatsInReservations(string resId)
            => data
                .CatsReservations
                .Where(cr => cr.ReservationId == resId)
                .Select(cr => new ResCatViewModel()
                {
                    Name = cr.Cat.Name,
                    BreedName = cr.Cat.Breed.Name
                })
                .ToList();


        public IEnumerable<ResViewModel> GetReservations(string userId)
        {
            var reservations = new List<ResViewModel>();
            var reservationsWoCats = data
                .Reservations
                .Where(r => r.UserId == userId)
                .Select(r => new ResViewModel()
                {
                    Id = r.Id,
                    DateOfReservation = r.DateOfReservation,
                    Arrival = r.Arrival.ToString("MM/dd/yyyy"),
                    Departure = r.Departure.ToString("MM/dd/yyyy"),
                    RoomType = new ResRoomTypeViewModel()
                    {
                        Name = r.RoomType.Name
                    },
                    Payment = new ResPaymentViewModel()
                    {
                        TotalPrice = $"$ {r.Payment.TotalPrice:f2}"
                    },
                    IsActive = r.IsActive
                })
                .OrderByDescending(r => r.DateOfReservation)
                .ToList();

            foreach (var res in reservationsWoCats)
            {
                res.Cats = GetCatsInReservations(res.Id);
                reservations.Add(res);
            }

            FilterReservations(reservations);


            return reservations;
        }

        public void FilterReservations(IEnumerable<ResViewModel> reservations)
        {
            foreach (var res in reservations)
            {
                if (ConvertToDateTime(res.Arrival) >= DateTime.UtcNow ||
                    !res.IsActive) continue;
                res.IsActive = false;
                data.Reservations.FirstOrDefault(r => r.Id == res.Id).IsActive = false;
                data.SaveChanges();
            }
        }

        public DateTime ConvertToDateTime(string dateString)
            => DateTime.ParseExact(dateString, "MM/dd/yyyy", CultureInfo.InvariantCulture);

    }
}