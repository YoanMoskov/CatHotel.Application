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
            SpecialNeeds = this.GetCatSpecialNeeds()
        });

        [HttpPost]
        [Authorize]
        public IActionResult Add(AddCatFormModel cat)
        {
            if (!this.data.Breeds.Any(b => b.BreedId == cat.BreedId))
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
        public IActionResult All()
        {
            var userCats = data.Cats
                .Where(c => c.UserId == userService.CurrentlyLoggedUser(User).Id)
                .Select(c => new CatViewModel()
                {
                    CatId = c.CatId,
                    Name = c.Name,
                    Age = c.Age,
                    PhotoUrl = c.PhotoUrl,
                    Breed = new CatBreedViewModel()
                    {
                        Name = c.Breed.Name
                    }
                }).ToList();

            return View(userCats);
        }

        private IEnumerable<CatBreedViewModel> GetCatBreeds()
            => this.data
                .Breeds
                .Select(c => new CatBreedViewModel()
                {
                    Id = c.BreedId,
                    Name = c.Name
                })
                .ToList();
    }
}