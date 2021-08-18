namespace CatHotel.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Infrastructure.Extensions;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Caching.Memory;
    using Models.Reservation.FormModels;
    using Services.CatService;
    using Services.Models.Reservations.CommonArea;
    using Services.ReservationService;
    using static WebConstants;

    [Authorize(Roles = UserRoleName)]
    public class ReservationsController : Controller
    {
        private readonly IMemoryCache _cache;
        private readonly ICatService _catService;
        private readonly IReservationService _resService;

        public ReservationsController(IReservationService resService, ICatService catService, IMemoryCache memoryCache)
        {
            _resService = resService;
            _catService = catService;
            _cache = memoryCache;
        }

        public IActionResult Create()
        {
            if (!_catService.UserHasCats(User.GetId()))
            {
                TempData[GlobalMessageKey] = "You have to add a cat before creating a reservation.";

                return RedirectToAction("Add", "Cats");
            }

            var roomTypes = _cache.Get<IEnumerable<ResRoomTypeServiceModel>>(roomTypesCacheKey);

            if (roomTypes == null)
            {
                roomTypes = _resService.RoomTypes();

                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(60));

                _cache.Set(roomTypesCacheKey, roomTypes, cacheOptions);
            }


            return View(new ResFormModel
            {
                Cats = _resService.CatsSelectList(User.GetId()),
                RoomTypes = roomTypes
            });
        }

        [HttpPost]
        public IActionResult Create(ResFormModel res)
        {
            if (res == null) return BadRequest();

            if (!_resService.DoesRoomTypeExist(res.RoomTypeId)) return BadRequest();

            if (res.CatIds != null && _resService.AreCatsInResTimeFrame(res.CatIds, res.Arrival, res.Departure) !=
                string.Empty)
                ModelState.AddModelError("CatIds",
                    _resService.AreCatsInResTimeFrame(res.CatIds, res.Arrival, res.Departure));

            if (!ModelState.IsValid)
                return View(new ResFormModel
                {
                    Cats = _resService.CatsSelectList(User.GetId()),
                    RoomTypes = _resService.RoomTypes()
                });

            _resService.Create(res.Arrival, res.Departure, res.RoomTypeId, res.CatIds, User.GetId());

            return RedirectToAction("All");
        }

        public IActionResult All()
        {
            var resCollection =
                _resService
                    .All(User.GetId())
                    .ToList();
            return View(resCollection);
        }
    }
}