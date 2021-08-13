namespace CatHotel.Areas.Admin.Controllers
{
    using AutoMapper;
    using Data;
    using Microsoft.AspNetCore.Mvc;
    using Models.Cats;
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

        public IActionResult All([FromQuery] AdminAllCatsQueryModel query)
        {
            var queryRes = _catService.AdminAll(
                query.Breed,
                query.CurrentPage,
                query.Sorting,
                query.Filtering,
                AdminAllCatsQueryModel.CatsPerPage);

            var catBreeds = _catService.GetBreeds();

            query.Breeds = catBreeds;
            query.TotalCats = queryRes.TotalCats;
            query.Cats = queryRes.Cats;

            return View(query);
        }

        public IActionResult Edit(string catId)
        {
            var cat = _catService.AdminGet(catId);
            cat.Breeds = _catService.GetBreeds();

            return View(cat);
        }

        [HttpPost]
        public IActionResult Edit(AdminEditCatFormModel c, string catId)
        {
            var cat = _catService.AdminGet(catId);
            cat.Breeds = _catService.GetBreeds();

            if (!ModelState.IsValid)
            {
                return View(cat);
            }

            _catService.AdminEdit(c.Name, c.Age, c.PhotoUrl, c.BreedId, c.AdditionalInformation, catId);

            return RedirectToAction("All");
        }

        public IActionResult Restore(string catId)
        {
            if (!User.IsInRole(AdminConstants.AdminRoleName))
            {
                return Unauthorized();
            }

            _catService.AdminRestore(catId);

            return RedirectToAction("All");
        }
    }
}