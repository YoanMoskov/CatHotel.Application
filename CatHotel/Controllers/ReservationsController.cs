namespace CatHotel.Controllers
{
    using Infrastructure;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Models.Reservation.FormModels;
    using Models.Reservation.ViewModels;
    using Services.CatService;
    using Services.ReservationService;
    using System.Linq;

    [Authorize]
    public class ReservationsController : Controller
    {
        private readonly IReservationService _resService;

        public ReservationsController(IReservationService resService, ICatService catService)
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
            if (res == null)
            {
                return BadRequest();
            }

            foreach (var catId in res.CatIds)
            {
                if (!_resService.DoesCatExist(catId))
                {
                    return BadRequest();
                }
            }

            if (!_resService.DoesRoomTypeExist(res.RoomTypeId))
            {
                return BadRequest();
            }

            if (_resService.AreCatsInResTimeFrame(res.CatIds, res.Arrival, res.Departure) != string.Empty)
            {
                ModelState.AddModelError("CatIds", _resService.AreCatsInResTimeFrame(res.CatIds, res.Arrival, res.Departure));
            }

            if (!ModelState.IsValid)
            {
                return View(new ResFormModel()
                {
                    Cats = _resService.CatsSelectList(User.GetId()),
                    RoomTypes = _resService.RoomTypes()
                });
            }

            _resService.Create(res.Arrival, res.Departure, res.RoomTypeId, res.CatIds, User.GetId());

            return RedirectToAction("All");
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
                    RoomTypeName = r.RoomTypeName,
                    TotalPrice = r.TotalPrice,
                    Cats = r.Cats,
                    ReservationState = r.ReservationState,
                    IsApproved = r.IsApproved
                })
                .ToList();
            return View(resCollection);
        }
    }
}