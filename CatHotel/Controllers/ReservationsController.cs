namespace CatHotel.Controllers
{
    using Data.Models.Enums;
    using Infrastructure;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Models.Reservation.FormModels;
    using Services.ReservationService;
    using System.Linq;
    using Services.CatService;
    using static WebConstants;

    [Authorize(Roles = UserRoleName)]
    public class ReservationsController : Controller
    {
        private readonly IReservationService _resService;
        private readonly ICatService _catService;

        public ReservationsController(IReservationService resService, ICatService catService)
        {
            this._resService = resService;
            _catService = catService;
        }

        public IActionResult Create()
        {
            if (!_catService.UserHasCats(User.GetId()))
            {
                TempData[GlobalMessageKey] = "You have to add a cat before creating a reservation.";

                return RedirectToAction("Add", "Cats");
            }

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

            if (!_resService.DoesRoomTypeExist(res.RoomTypeId))
            {
                return BadRequest();
            }

            if (res.CatIds != null && _resService.AreCatsInResTimeFrame(res.CatIds, res.Arrival, res.Departure) != string.Empty)
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

            return RedirectToAction("PendingApproval");
        }

        public IActionResult Active()
        {
            var resCollection =
                _resService
                    .AllWithState(User.GetId(), ReservationState.Active, true)
                    .ToList();
            return View(resCollection);
        }

        public IActionResult Approved()
        {
            var resCollection =
                _resService
                    .AllWithState(User.GetId(), ReservationState.Pending, true)
                    .ToList();
            return View(resCollection);
        }

        public IActionResult PendingApproval()
        {
            var resCollection =
                _resService
                    .AllWithState(User.GetId(), ReservationState.Pending, false)
                    .ToList();
            return View(resCollection);
        }
    }
}