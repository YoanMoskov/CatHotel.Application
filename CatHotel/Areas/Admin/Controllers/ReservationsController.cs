namespace CatHotel.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Models.Reservations;
    using Services.ReservationService;

    public class ReservationsController : AdminController
    {
        private readonly IReservationService _resService;

        public ReservationsController(IReservationService service)
        {
            _resService = service;
        }

        public IActionResult All([FromQuery] AdminAllReservationsQueryModel query)
        {
            var queryRes = _resService.AdminAll(
                query.RoomType,
                query.CurrentPage,
                query.Sorting,
                query.Filtering,
                AdminAllReservationsQueryModel.ResPerPage);

            var roomTypes = _resService.RoomTypes();

            query.RoomTypes = roomTypes;
            query.TotalReservations = queryRes.TotalReservations;
            query.Reservations = queryRes.Reservations;

            return View(query);
        }

        public IActionResult Approve(string resId)
        {
            if (!_resService.AdminApprove(resId))
            {
                return BadRequest();
            }

            return RedirectToAction("All");
        }
    }
}