namespace CatHotel.Controllers
{
    using Models.Cat;
    using Data;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Services.CatService;
    using Services.UserServices;
    using System.Collections.Generic;
    using System.Linq;
    using Models.Cat.FormModel;
    using Models.Cat.ViewModel;

    public class CatsController : Controller
    {
        private readonly ApplicationDbContext data;
        private readonly IUserService userService;
        private readonly ICatService catService;

        public CatsController(ApplicationDbContext data, IUserService userService, ICatService catService)
        {
            this.data = data;
            this.userService = userService;
            this.catService = catService;
        }

        [Authorize]
        public IActionResult Add() => View(new AddCatFormModel()
        {
            Breeds = this.GetCatBreeds(),
        });

        [HttpPost]
        [Authorize]
        public IActionResult Add(AddCatFormModel cat)
        {
            if (!this.data.Breeds.Any(b => b.Id == cat.BreedId))
            {
                this.ModelState.AddModelError(nameof(cat.BreedId), "Breed does not exist.");
            }

            if (!ModelState.IsValid)
            {
                cat.Breeds = GetCatBreeds();
                return View(cat);
            }

            var loggedUser = userService.CurrentlyLoggedUser(User);

            catService.AddCat(cat, loggedUser);

            return RedirectToAction("All");
        }

        [Authorize]
        public IActionResult All() => View(catService.GetAllCatsCatViewModels(userService.CurrentlyLoggedUser(User).Id));


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


        private IEnumerable<CatBreedViewModel> GetCatBreeds()
            => this.data
                .Breeds
                .Select(c => new CatBreedViewModel()
                {
                    Id = c.Id,
                    Name = c.Name
                })
                .ToList();
    }
}