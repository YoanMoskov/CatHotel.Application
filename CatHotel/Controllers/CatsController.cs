namespace CatHotel.Controllers
{
    using AutoMapper;
    using Data.Models;
    using Infrastructure;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Models.Cat.FormModel;
    using Models.Cat.ViewModel;
    using Services.CatService;
    using System.Linq;

    [Authorize]
    public class CatsController : Controller
    {
        private readonly ICatService _catService;
        private readonly IMapper _mapper;

        public CatsController(
            ICatService catService,
            IMapper mapper)
        {
            this._catService = catService;
            this._mapper = mapper;
        }

        public IActionResult Add() => View(new AddCatFormModel()
        {
            Breeds = _catService.GetBreeds()
        });

        [HttpPost]
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

            var catForm = _mapper.Map<Cat>(cat);

            _catService.Add(
                catForm,
                User.GetId());

            return RedirectToAction("All");
        }

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

        public IActionResult Edit(string catId)
        {
            var cat = _catService.Details(catId);

            var catEditForm = _mapper.Map<CatViewModel>(cat);

            return View(catEditForm);
        }

        [HttpPost]
        public IActionResult Edit(EditCatFormModel c, string catId)
        {
            var cat = _catService.Details(catId);

            var catEditForm = _mapper.Map<CatViewModel>(cat);

            if (!ModelState.IsValid)
            {
                return View(catEditForm);
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