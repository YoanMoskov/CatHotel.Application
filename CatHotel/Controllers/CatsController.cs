namespace CatHotel.Controllers
{
    using System;
    using System.Collections.Generic;
    using AutoMapper;
    using Infrastructure.Extensions;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Caching.Memory;
    using Models.Cat.FormModel;
    using Services.CatService;
    using Services.Models.Cats.CommonArea;
    using static WebConstants;

    [Authorize(Roles = UserRoleName)]
    public class CatsController : Controller
    {
        private readonly IMemoryCache _cache;
        private readonly ICatService _catService;
        private readonly IMapper _mapper;

        public CatsController(
            ICatService catService,
            IMapper mapper, IMemoryCache cache)
        {
            _catService = catService;
            _mapper = mapper;
            _cache = cache;
        }

        public IActionResult Add()
        {
            var breeds = _cache.Get<IEnumerable<CatBreedServiceModel>>(breedsCacheKey);

            if (breeds == null)
            {
                breeds = _catService.GetBreeds();

                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(60));

                _cache.Set(breedsCacheKey, breeds, cacheOptions);
            }

            return View(new AddCatFormModel
            {
                Breeds = breeds
            });
        }

        [HttpPost]
        public IActionResult Add(AddCatFormModel cat)
        {
            if (!_catService.DoesBreedExist(cat.BreedId))
                ModelState.AddModelError(nameof(cat.BreedId), "Breed does not exist.");

            if (!ModelState.IsValid)
            {
                cat.Breeds = _catService.GetBreeds();
                return View(cat);
            }

            _catService.Add(
                _mapper.Map<CatServiceModel>(cat),
                User.GetId());

            return RedirectToAction("All");
        }

        public IActionResult All()
        {
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
            if (c == null) return BadRequest();

            var cat = _catService.Get(catId);

            var catEditForm = _mapper.Map<CatServiceModel>(cat);

            if (!ModelState.IsValid) return View(catEditForm);

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