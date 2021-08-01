namespace CatHotel.Controllers
{
    using Infrastructure;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Models.Cat.FormModel;
    using Models.Cat.ViewModel;
    using Services.CatService;
    using System.Linq;

    public class CatsController : Controller
    {
        private readonly ICatService _catService;

        public CatsController(
            ICatService catService)
        {
            this._catService = catService;
        }

        [Authorize]
        public IActionResult Add() => View(new AddCatFormModel()
        {
            Breeds = _catService.GetBreeds()
        });

        [HttpPost]
        [Authorize]
        public IActionResult Add(AddCatFormModel cat)
        {
            if (!_catService.DoesBreedExist(cat.BreedId))
            {
                this.ModelState.AddModelError(nameof(cat.BreedId), "Breed does not exist.");
            }

            if (!ModelState.IsValid)
            {
                cat.Breeds = _catService.GetBreeds();
                return View(cat);
            }

            _catService.Add(
                cat.Name,
                cat.Age,
                cat.PhotoUrl,
                cat.BreedId,
                cat.AdditionalInformation,
                User.GetId());

            return RedirectToAction("All");
        }

        [Authorize]
        public IActionResult All()
        {
            var catCollection = _catService.All(User.GetId())
                .Select(cat => new CatViewModel()
                {
                    Id = cat.Id,
                    Name = cat.Name,
                    Age = cat.Age,
                    PhotoUrl = cat.PhotoUrl,
                    BreedName = cat.BreedName
                })
                .ToList();

            return View(catCollection);
        }

        [Authorize]
        public IActionResult Edit(string catId)
        {
            var cat = _catService.Details(catId);

            return View(new CatViewModel()
            {
                Id = cat.Id,
                Name = cat.Name,
                Age = cat.Age,
                PhotoUrl = cat.PhotoUrl,
                AdditionalInformation = cat.AdditionalInformation,
                BreedName = cat.BreedName
            });
        }

        [HttpPost]
        [Authorize]
        public IActionResult Edit(EditCatFormModel c, string catId)
        {
            var cat = _catService.Details(catId);
            if (!ModelState.IsValid)
            {
                return View(new CatViewModel()
                {
                    Id = cat.Id,
                    Name = cat.Name,
                    Age = cat.Age,
                    PhotoUrl = cat.PhotoUrl,
                    AdditionalInformation = cat.AdditionalInformation,
                    BreedName = cat.BreedName
                });
            }

            _catService.Edit(c.Age, c.PhotoUrl, c.AdditionalInformation, catId);

            return RedirectToAction("All");
        }

        public IActionResult Delete(string catId)
        {
            _catService.Delete(catId);

            return RedirectToAction("All");
        }
    }
}