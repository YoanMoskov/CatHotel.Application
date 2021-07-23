namespace CatHotel.Controllers
{
    using Data;
    using Infrastructure;
    using Models.Reservation.FormModels;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;
    using Services.UserService;
    using Services.ReservationServices;

    public class ReservationController : Controller
    {
        private readonly IReservationService resService;

        public ReservationController(IReservationService resService)
        {
            this.resService = resService;
        }

        [Authorize]
        public IActionResult Create()
        {
            return View(new ResFormModel()
            {
                Cats = resService.GetCatsSelectList(User.GetId()),
                RoomTypes = resService.GetRoomTypes()
            });
        }

        [HttpPost]
        [Authorize]
        public IActionResult Create(ResFormModel res)
        {
            if (!ModelState.IsValid)
            {
                return View(new ResFormModel()
                {
                    Cats = resService.GetCatsSelectList(User.GetId()),
                    RoomTypes = resService.GetRoomTypes()
                });
            }

            resService.CreateReservation(res, User.GetId());

            return RedirectToAction("All", "Cats");
        }

        [Authorize]
        public IActionResult All() => View(resService.GetReservations(User.GetId()));
    }
}