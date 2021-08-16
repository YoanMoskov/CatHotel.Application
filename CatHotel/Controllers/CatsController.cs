namespace CatHotel.Controllers
{
    using Areas.Admin;
    using AutoMapper;
    using Data.Models;
    using Infrastructure;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Models.Cat.FormModel;
    using Services.CatService;
    using Services.Models.Cats.CommonArea;

    using static WebConstants;

    [Authorize(Roles = UserRoleName)]
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
            if (!_catService.UserHasCats(User.GetId()) && !User.IsInRole(AdminConstants.AdminRoleName))
            {
                return RedirectToAction("Add");
            }

            var catCollection = _catService.All(User.GetId());

            return View(catCollection);
        }

        public IActionResult Edit(string catId)
        {
            var cat = _catService.Get(catId);

            return View(cat);
        }

        [HttpPost]
        public IActionResult Edit(EditCatFormModel c, string catId)
        {
            if (c == null)
            {
                return BadRequest();
            }

            var cat = _catService.Get(catId);

            var catEditForm = _mapper.Map<CatServiceModel>(cat);

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