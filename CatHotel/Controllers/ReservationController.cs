namespace CatHotel.Controllers
{
    using Data;
    using Data.Models;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Models.Reservation.ViewModels;
    using Models.RoomType;
    using Services.UserServices;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.AspNetCore.Authorization;
    using Models.Reservation.FormModels;

    public class ReservationController : Controller
    {
        private readonly ApplicationDbContext data;
        private readonly IUserService userService;

        public ReservationController(ApplicationDbContext data, IUserService userService)
        {
            this.data = data;
            this.userService = userService;
        }

        [Authorize]
        public IActionResult Create() => View(new ReservationFormModel()
        {
            Cats = this.GetCatsSelectList(),
            RoomTypes = this.GetRoomTypes()
        });

        [HttpPost]
        [Authorize]
        public IActionResult Create(ReservationFormModel res)
        {
            var newReservation = new Reservation()
            {
                DateOfReservation = DateTime.UtcNow.ToLocalTime(),
                Arrival = res.Arrival,
                Departure = res.Departure,
                RoomTypeId = res.RoomTypeId,
                UserId = userService.CurrentlyLoggedUser(User).Id
            };

            foreach (var catId in res.CatIds)
            {
                var cat = data.Cats.FirstOrDefault(c => c.Id == catId);
                newReservation.Cats.Add(cat);
            }

            data.Reservations.Add(newReservation);
            data.SaveChanges();

            return RedirectToAction("All", "Cats");
        }

        [Authorize]
        public IActionResult All() => View(GetReservations());

        private IEnumerable<SelectListItem> GetCatsSelectList()
            => this.data
                .Cats
                .Where(c => c.UserId == userService.CurrentlyLoggedUser(User).Id)
                .Select(c => new SelectListItem()
                {
                    Value = c.Id,
                    Text = $"{c.Name} - {c.Breed.Name}"
                })
                .ToList();

        private IEnumerable<RoomTypeViewModel> GetRoomTypes()
            => this.data
                .RoomTypes
                .Select(rt => new RoomTypeViewModel()
                {
                    Id = rt.Id,
                    Name = rt.Name
                })
                .ToList();

        private IEnumerable<ResCatViewModel> GetCatsInReservations(string resId)
            => data.Cats
                .Where(c => c.ReservationId == resId)
                .Select(c => new ResCatViewModel()
                {
                    Name = c.Name,
                    BreedName = c.Breed.Name
                });

        private IEnumerable<ReservationViewModel> GetReservations()
        {
            var resevations = data.Reservations
                .Where(r => r.UserId == userService.CurrentlyLoggedUser(User).Id)
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