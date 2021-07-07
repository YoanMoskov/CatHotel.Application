namespace CatHotel.Controllers
{
    using System.Linq;
    using Data;
    using Data.Models;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Services.CatService;
    using Services.UserServices;
    using ViewModels.Cat;

    public class CatsController : Controller
    {
        private readonly ApplicationDbContext data;
        private readonly UserManager<User> userManager;
        private readonly IUserService userService;
        private readonly ICatService catService;

        public CatsController(ApplicationDbContext data, IUserService userService, ICatService catService, UserManager<User> userManager)
        {
            this.data = data;
            this.userService = userService;
            this.catService = catService;
            this.userManager = userManager;
        }

        [Authorize]
        public IActionResult Create()
        {
            var breedList = catService.BreedsAsSelectListItems();

            catService.InsertOptionSelectBreed(breedList);

            ViewBag.BreedList = breedList;

            return View();
        }

        [HttpPost]
        [Authorize]
        public IActionResult Create(CatFormModel c)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var loggedUser = userService.CurrentlyLoggedUser(User);

            catService.AddCat(c, loggedUser);

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
    }
}