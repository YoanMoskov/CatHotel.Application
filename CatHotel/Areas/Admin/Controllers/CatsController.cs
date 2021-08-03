namespace CatHotel.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class CatsController : AdminController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}