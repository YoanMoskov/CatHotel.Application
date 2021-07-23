namespace CatHotel.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class GroomingController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}