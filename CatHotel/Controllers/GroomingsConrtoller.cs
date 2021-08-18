namespace CatHotel.Controllers
{
    using System;
    using System.Collections.Generic;
    using Infrastructure.Extensions;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Caching.Memory;
    using Models.Grooming.FormModel;
    using Services.CatService;
    using Services.GroomingService;
    using Services.Models.Groomings.CommonArea;
    using static WebConstants;

    [Authorize(Roles = UserRoleName)]
    public class GroomingsController : Controller
    {
        private readonly IMemoryCache _cache;
        private readonly ICatService _catService;
        private readonly IGroomingService _groomingService;

        public GroomingsController(
            ICatService catService,
            IGroomingService groomingService, IMemoryCache cache)
        {
            _catService = catService;
            _groomingService = groomingService;
            _cache = cache;
        }

        public IActionResult Styles()
        {
            if (!_catService.UserHasCats(User.GetId()))
            {
                TempData[GlobalMessageKey] = "You need to add a cat before reserving a groom.";

                return RedirectToAction("Add", "Cats");
            }

            var styles = _cache.Get<IEnumerable<GroomingStyleServiceModel>>(stylesCacheKey);

            if (styles == null)
            {
                styles = _groomingService.GetStyles();

                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(60));

                _cache.Set(stylesCacheKey, styles, cacheOptions);
            }

            return View(styles);
        }

        public IActionResult Cats(int styleId)
            => View(_groomingService.GetCatsOfUser(User.GetId(), styleId));

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