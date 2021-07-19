namespace CatHotel.Controllers
{
    using Data;
    using Models.Reservation.FormModels;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;
    using Services.UserService;
    using Services.ReservationServices;

    public class ReservationController : Controller
    {
        private readonly ApplicationDbContext data;
        private readonly IUserService userService;
        private readonly IReservationService resService;

        public ReservationController(ApplicationDbContext data, IUserService userService, IReservationService resService)
        {
            this.data = data;
            this.userService = userService;
            this.resService = resService;
        }

        [Authorize]
        public IActionResult Create()
        {
            return View(new ReservationFormModel()
            {
                Cats = resService.GetCatsSelectList(userService.UserId(User)),
                RoomTypes = resService.GetRoomTypes()
            });
        }

        [HttpPost]
        [Authorize]
        public IActionResult Create(ReservationFormModel res)
        {
            if (!ModelState.IsValid)
            {
                return View(new ReservationFormModel()
                {
                    Cats = resService.GetCatsSelectList(userService.UserId(User)),
                    RoomTypes = resService.GetRoomTypes()
                });
            }

            resService.CreateReservation(res, userService.UserId(User));

            return RedirectToAction("All", "Cats");
        }

        [Authorize]
        public IActionResult All() => View(resService.GetReservations(userService.UserId(User)));
    }
}