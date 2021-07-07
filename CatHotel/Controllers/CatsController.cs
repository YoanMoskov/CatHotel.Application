namespace CatHotel.Controllers
{
    using Data;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using System.Linq;
    using System.Security.Claims;
    using Data.Models;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using ViewModels.Cat;

    public class CatsController : Controller
    {
        private readonly ApplicationDbContext data;

        public CatsController(ApplicationDbContext data)
        {
            this.data = data;
        }

        [Authorize]
        public IActionResult Create()
        {
            var breedList = (from breed in data.Breeds
                select new SelectListItem()
                {
                    Text = breed.Name,
                    Value = breed.BreedId.ToString()
                })
                .ToList();
            breedList.Insert(0, new SelectListItem()
            {
                Text = "Select breed",
                Value = string.Empty
            });
            ViewBag.BreedList = breedList;

            return View();
        }

        [HttpPost]
        public IActionResult Create(CatFormModel c)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var loggedUser = data.Users
                .FirstOrDefault(u => u.Id == userId);

            var cat = new Cat()
            {
                Name = c.Name,
                Age = c.Age,
                PhotoUrl = c.PhotoUrl,
                BreedId = c.BreedId,
            };

            loggedUser.Cats.Add(cat);
            data.SaveChanges();

           return RedirectToAction("All");
        }

        public IActionResult All()
        {
            return View();
        }
    }
}