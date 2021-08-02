namespace CatHotel.Controllers
{
    using System.Linq;
    using Infrastructure;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Models.Reservation.FormModels;
    using Models.Reservation.ViewModels;
    using Services.ReservationService;

    [Authorize]
    public class ReservationController : Controller
    {
        private readonly IReservationService _resService;

        public ReservationController(IReservationService resService)
        {
            this._resService = resService;
        }

        public IActionResult Create()
        {
            return View(new ResFormModel()
            {
                Cats = _resService.CatsSelectList(User.GetId()),
                RoomTypes = _resService.RoomTypes()
            });
        }

        [HttpPost]
        public IActionResult Create(ResFormModel res)
        {
            if (!ModelState.IsValid)
            {
                return View(new ResFormModel()
                {
                    Cats = _resService.CatsSelectList(User.GetId()),
                    RoomTypes = _resService.RoomTypes()
                });
            }

            _resService.Create(res.Arrival, res.Departure, res.RoomTypeId, res.CatIds, User.GetId());

            return RedirectToAction("All", "Cats");
        }

        public IActionResult All()
        {
            var resCollection = _resService.All(User.GetId())
                .Select(r => new ResViewModel()
                {
                    Id = r.Id,
                    DateOfReservation = r.DateOfReservation,
                    Arrival = r.Arrival,
                    Departure = r.Departure,
                    Cats = r.Cats,
                    RoomTypeName = r.RoomTypeName,
                    TotalPrice = r.TotalPrice,
                    IsActive = r.IsActive
                })
                .ToList();
            return View(resCollection);
        }
    }
}