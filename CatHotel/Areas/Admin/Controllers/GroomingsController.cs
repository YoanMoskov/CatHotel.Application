namespace CatHotel.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Models.Groomings;
    using Services.GroomingService;

    public class GroomingsController : AdminController
    {
        private readonly IGroomingService _groomingService;

        public GroomingsController(IGroomingService groomingService)
        {
            _groomingService = groomingService;
        }

        public IActionResult All([FromQuery] AdminAllGroomingsQueryModel query)
        {
            var queryRes = _groomingService.AdminAll(
                query.StyleId,
                query.CurrentPage,
                query.Sorting,
                query.Filtering,
                AdminAllGroomingsQueryModel.GroomsPerPage);

            query.Styles = _groomingService.GetStyles();
            query.TotalGroomings = queryRes.TotalGroomings;
            query.Grooms = queryRes.Groomings;

            return View(query);
        }

        public IActionResult Approve(string groomId)
        {
            if (!_groomingService.AdminApprove(groomId)) return BadRequest();

            return RedirectToAction("All");
        }
    }
}