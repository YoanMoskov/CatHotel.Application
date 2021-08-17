namespace CatHotel.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Data;
    using Data.Models;
    using Infrastructure;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Models.Grooming.FormModel;
    using Models.Grooming.ViewModel;
    using Services.CatService;

    using static WebConstants;

    [Authorize(Roles = UserRoleName)]
    public class GroomingsController : Controller
    {
        private readonly ApplicationDbContext _data;
        private readonly IMapper _mapper;
        private readonly ICatService _catService;

        public GroomingsController(ApplicationDbContext data, IMapper mapper, ICatService catService)
        {
            _data = data;
            _mapper = mapper;
            _catService = catService;
        }

        public IActionResult Styles()
        {
            if (!_catService.UserHasCats(User.GetId()))
            {
                TempData[GlobalMessageKey] = "You need to add a cat before reserving a groom.";

                return RedirectToAction("Add", "Cats");
            }
            return View(GetStyles());
        }

        public IActionResult Cats(int styleId)
        {
            return View(GetCatsOfUser(User.GetId(), styleId));
        }

        public IActionResult Complete(int styleId, string catId)
        {
            var style = SelectedStyle(styleId);

            var cat = SelectedCat(catId);

            if (style == null)
            {
                return BadRequest();
            }

            if (cat == null)
            {
                return BadRequest();
            }

            return View(new AddGroomingModel()
            {
                Cat = cat,
                Style = style
            });
        }

        [HttpPost]
        public IActionResult Complete(AddGroomingModel g, int styleId, string catId)
        {
            var style = SelectedStyle(styleId);

            var cat = SelectedCat(catId);

            if (style == null)
            {
                return BadRequest();
            }

            if (cat == null)
            {
                return BadRequest();
            }

            var grooming = new Grooming()
            {
                StyleId = style.Id,
                Appointment = g.Appointment
            };

            var catGroom = new CatGrooming()
            {
                CatId = cat.Id,
                GroomingId = grooming.Id
            };

            _data.Groomings.Add(grooming);
            _data.CatsGroomings.Add(catGroom);
            _data.SaveChanges();

            return RedirectToAction("All");
        }

        public IActionResult All()
            => View();

        private IEnumerable<GroomingCatViewModel> GetCatsOfUser(string userId, int styleId )
        {
            return _data.Cats
                .Where(c => c.UserId == userId)
                .Select(c => new GroomingCatViewModel()
                {
                    Id = c.Id,
                    Name = c.Name,
                    BreedName = c.Breed.Name,
                    PhotoUrl = c.PhotoUrl,
                    StyleId = styleId
                })
                .ToList();
        }

        private IEnumerable<GroomingStyleModel> GetStyles()
        {
            return _data.Styles
                .ProjectTo<GroomingStyleModel>(_mapper.ConfigurationProvider)
                .ToList();
        }

        private GroomingStyleModel SelectedStyle(int styleId)
            => _data.Styles
                .Where(s => s.Id == styleId)
                .ProjectTo<GroomingStyleModel>(this._mapper.ConfigurationProvider)
                .FirstOrDefault();

        private GroomingCatViewModel SelectedCat(string catId)
            => _data.Cats
                .Where(c => c.Id == catId)
                .Select(c => new GroomingCatViewModel()
                {
                    Id = c.Id,
                    Name = c.Name,
                    BreedName = c.Breed.Name,
                    PhotoUrl = c.PhotoUrl,
                })
                .FirstOrDefault();
    }
}