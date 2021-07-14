namespace CatHotel.Controllers
{
    using Data;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Models.Reservation;
    using Models.RoomType;
    using Services.UserServices;
    using System.Collections.Generic;
    using System.Linq;
    using Data.Models;
    using Models.Cat;

    public class ReservationController : Controller
    {
        private readonly ApplicationDbContext data;
        private readonly IUserService userService;

        public ReservationController(ApplicationDbContext data, IUserService userService)
        {
            this.data = data;
            this.userService = userService;
        }

        public IActionResult Create() => View(new ReservationFormModel()
        {
            Cats = this.GetCats(),
            RoomTypes = this.GetRoomTypes()
        });

        [HttpPost]
        public IActionResult Create(ReservationFormModel res)
        {
            var newReservation = new Reservation()
            {
                DateOfReservation = res.DateOfReservation,
                Arrival = res.Arrival,
                Departure = res.Departure,
                RoomTypeId = res.RoomTypeId
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

        private IEnumerable<SelectListItem> GetCats()
            => this.data
                .Cats
                .Where(c => c.UserId == userService.CurrentlyLoggedUser(User).Id)
                .Select(c => new SelectListItem()
                {
                    Value = c.Id,
                    Text = $"{c.Name} - {c.Breed.Name}"
                })
                .ToList();

        public IActionResult All() => View();

        private IEnumerable<RoomTypeViewModel> GetRoomTypes()
            => this.data
                .RoomTypes
                .Select(rt => new RoomTypeViewModel()
                {
                    Id = rt.Id,
                    Name = rt.Name
                })
                .ToList();
    }
}