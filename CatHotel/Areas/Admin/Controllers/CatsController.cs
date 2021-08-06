namespace CatHotel.Areas.Admin.Controllers
{
    using System.Linq;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Data;
    using Microsoft.AspNetCore.Mvc;
    using Models;
    using Services.CatService;

    public class CatsController : AdminController
    {
        private readonly ICatService _catService;
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _data;

        public CatsController(
            ICatService catService,
            IMapper mapper, ApplicationDbContext data)
        {
            _catService = catService;
            _mapper = mapper;
            _data = data;
        }

        public IActionResult All()
        {
            var cats = _catService.AdminAll();
            return View(cats);
        }

        public IActionResult Edit(string catId)
        {
            var cat = _data.Cats
                .Where(c => c.Id == catId)
                .ProjectTo<AdminCatEditViewModel>(_mapper.ConfigurationProvider)
                .FirstOrDefault();
            cat.Breeds = _catService.GetBreeds();

            return View(cat);
        }

        [HttpPost]
        public IActionResult Edit(AdminEditCatFormModel c, string catId)
        {
            var cat = _catService.Cat(catId);

            if (!ModelState.IsValid)
            {
                return View(cat);
            }

            _catService.AdminEdit(c.Name, c.Age, c.PhotoUrl, c.BreedId, c.AdditionalInformation, catId);

            return RedirectToAction("All");
        }

        public IActionResult Restore(string catId)
        {
            if (!User.IsInRole(AdminConstants.RoleName))
            {
                return Unauthorized();
            }

            _catService.AdminRestore(catId);

            return RedirectToAction("All");
        }
    }
}