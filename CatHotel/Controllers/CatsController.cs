namespace CatHotel.Controllers
{
    using Data;
    using Infrastructure;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Models.Cat.FormModel;
    using Services.CatService;
    using Services.UserService;

    public class CatsController : Controller
    {
        private readonly IUserService userService;
        private readonly ICatService catService;

        public CatsController(ApplicationDbContext data, IUserService userService, ICatService catService)
        {
            this.userService = userService;
            this.catService = catService;
        }

        [Authorize]
        public IActionResult Add() => View(new AddCatFormModel()
        {
            Breeds = catService.GetCatBreeds()
        });

        [HttpPost]
        [Authorize]
        public IActionResult Add(AddCatFormModel cat)
        {
            if (!catService.DoesBreedExist(cat.BreedId))
            {
                this.ModelState.AddModelError(nameof(cat.BreedId), "Breed does not exist.");
            }

            if (!ModelState.IsValid)
            {
                cat.Breeds = catService.GetCatBreeds();
                return View(cat);
            }

            catService.AddCat(cat, userService.CurrentlyLoggedUser(User));

            return RedirectToAction("All");
        }

        [Authorize]
        public IActionResult All() => View(catService.GetAllCatsCatViewModels(User.GetId()));

        [Authorize]
        public IActionResult Edit(string catId) => View(catService.GetCatInViewModel(catId));

        [HttpPost]
        [Authorize]
        public IActionResult Edit(EditCatFormModel c, string catId)
        {
            if (!ModelState.IsValid)
            {
                return View(catService.GetCatInViewModel(catId));
            }

            catService.EditCat(c, catId);

            return RedirectToAction("All");
        }

        public IActionResult Delete(string catId)
        {
            catService.DeleteCat(catId);

            return RedirectToAction("All");
        }
    }
}