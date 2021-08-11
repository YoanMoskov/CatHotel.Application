namespace CatHotel.Controllers
{
    using Areas.Admin;
    using Data.Models.Enums;
    using Infrastructure;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Models.Reservation.FormModels;
    using Services.ReservationService;
    using System.Linq;

    [Authorize]
    public class ReservationsController : Controller
    {
        private readonly IReservationService _resService;

        public ReservationsController(IReservationService resService)
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

            return RedirectToAction("PendingApproval");
        }

        public IActionResult Active()
        {
            if (User.IsInRole(AdminConstants.RoleName))
            {
                return Redirect("/Admin/Reservations/All");
            }
            var resCollection =
                _resService
                    .AllWithState(User.GetId(), ReservationState.Active, true)
                    .ToList();
            return View(resCollection);
        }

        public IActionResult Approved()
        {
            if (User.IsInRole(AdminConstants.RoleName))
            {
                return Redirect("/Admin/Reservations/All");
            }
            var resCollection =
                _resService
                    .AllWithState(User.GetId(), ReservationState.Pending, true)
                    .ToList();
            return View(resCollection);
        }

        public IActionResult PendingApproval()
        {
            if (User.IsInRole(AdminConstants.RoleName))
            {
                return Redirect("/Admin/Reservations/All");
            }
            var resCollection =
                _resService
                    .AllWithState(User.GetId(), ReservationState.Pending, false)
                    .ToList();
            return View(resCollection);
        }
    }
}