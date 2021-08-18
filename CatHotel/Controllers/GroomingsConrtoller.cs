namespace CatHotel.Controllers
{
    using Infrastructure.Extensions;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Models.Grooming.FormModel;
    using Services.CatService;
    using Services.GroomingService;
    using static WebConstants;

    [Authorize(Roles = UserRoleName)]
    public class GroomingsController : Controller
    {
        private readonly ICatService _catService;
        private readonly IGroomingService _groomingService;

        public GroomingsController(
            ICatService catService,
            IGroomingService groomingService)
        {
            _catService = catService;
            _groomingService = groomingService;
        }

        public IActionResult Styles()
        {
            if (!_catService.UserHasCats(User.GetId()))
            {
                TempData[GlobalMessageKey] = "You need to add a cat before reserving a groom.";

                return RedirectToAction("Add", "Cats");
            }

            return View(_groomingService.GetStyles());
        }

        public IActionResult Cats(int styleId) => View(_groomingService.GetCatsOfUser(User.GetId(), styleId));

        public IActionResult Complete(int styleId, string catId)
            => View(new AddGroomingModel
            {
                Cat = _groomingService.SelectedCat(catId),
                Style = _groomingService.SelectedStyle(styleId)
            });

        [HttpPost]
        public IActionResult Complete(AddGroomingModel g, int styleId, string catId)
        {
            if (!_groomingService.DoesStyleExist(styleId)) return BadRequest();

            if (!_catService.DoesCatExist(catId)) return BadRequest();

            if (!ModelState.IsValid)
                return View(new AddGroomingModel
                {
                    Cat = _groomingService.SelectedCat(catId),
                    Style = _groomingService.SelectedStyle(styleId)
                });

            _groomingService.Create(catId, styleId, g.Appointment, User.GetId());

            return RedirectToAction("All");
        }

        public IActionResult All()
            => View(_groomingService.All(User.GetId()));
    }
}