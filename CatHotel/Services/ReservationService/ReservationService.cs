namespace CatHotel.Services.ReservationService
{
    using Areas.Admin.Models.Enums.Reservations;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Data;
    using Data.Models;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Models.Reservations.AdminArea;
    using Models.Reservations.CommonArea;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class ReservationService : IReservationService
    {
        private readonly ApplicationDbContext _data;
        private readonly IMapper _mapper;

        public ReservationService(ApplicationDbContext data, IMapper mapper)
        {
            this._data = data;
            _mapper = mapper;
        }

        public void Create(DateTime arrival, DateTime departure, int roomTypeId, string[] catIds, string userId)
        {
            var price = 0m;
            var room = _data.RoomTypes
                .FirstOrDefault(r => r.Id == roomTypeId);

            if (room != null)
            {
                price = room.PricePerDay * (departure - arrival).Days * catIds.Count();
            }

            var newReservation = new Reservation()
            {
                DateOfReservation = DateTime.UtcNow,
                Arrival = arrival,
                Departure = departure,
                RoomTypeId = roomTypeId,
                UserId = userId,
                Payment = new Payment()
                {
                    TotalPrice = price,
                    isPaid = false,
                },
                IsActive = true
            };

            var allCatsReservation = catIds.Select(catId
                => new CatReservation() { CatId = catId, Reservation = newReservation }).ToList();

            _data.CatsReservations.AddRange(allCatsReservation);
            _data.SaveChanges();
        }

        public IEnumerable<SelectListItem> CatsSelectList(string userId)
            => this._data
                .Cats
                .Where(c => c.UserId == userId && c.IsDeleted == false)
                .Select(c => new SelectListItem()
                {
                    Value = c.Id,
                    Text = $"{c.Name} - {c.Breed.Name}"
                })
                .ToList();

        public IEnumerable<ResRoomTypeServiceModel> RoomTypes()
            => this._data
                .RoomTypes
                .Select(rt => new ResRoomTypeServiceModel()
                {
                    Id = rt.Id,
                    Name = rt.Name
                })
                .ToList();

        public IEnumerable<ResServiceModel> All(string userId)
        {
            FilterReservations();

            var reservations = _data
                .Reservations
                .Where(r => r.UserId == userId && r.IsActive)
                .Select(r => new ResServiceModel()
                {
                    Id = r.Id,
                    DateOfReservation = r.DateOfReservation,
                    Arrival = r.Arrival.ToString("MM/dd/yyyy"),
                    Departure = r.Departure.ToString("MM/dd/yyyy"),
                    RoomTypeName = r.RoomType.Name,
                    TotalPrice = $"${r.Payment.TotalPrice:f2}",
                    IsActive = r.IsActive,
                    IsApproved = r.IsApproved
                })
                .OrderByDescending(r => r.DateOfReservation)
                .ToList();

            foreach (var res in reservations)
            {
                if (res != null)
                {
                    res.Cats = CatsInReservations(res.Id);
                }
            }

            return reservations;
        }

        public AdminQueryReservationServiceModel AdminAll(
            string roomName = null,
            int currentPage = 1,
            ResSorting sorting = ResSorting.Newest,
            ResFiltering filtering = ResFiltering.Pending,
            int resPerPage = Int32.MaxValue)
        {
            IQueryable<Reservation> resQuery = _data.Reservations;

            if (!string.IsNullOrWhiteSpace(roomName))
            {
                resQuery = resQuery.Where(r => r.RoomType.Id == int.Parse(roomName));
            }

            resQuery = filtering switch
            {
                ResFiltering.Approved => resQuery.Where(r => r.IsApproved),
                ResFiltering.Active => resQuery.Where(r => r.IsActive),
                ResFiltering.Expired => resQuery.Where(r => r.IsActive == false),
                ResFiltering.Pending or _ => resQuery.Where(r => r.IsApproved == false)
            };

            resQuery = sorting switch
            {
                ResSorting.Oldest => resQuery.OrderBy(r => r.DateOfReservation),
                ResSorting.Newest or _ => resQuery.OrderByDescending(r => r.DateOfReservation)
            };

            var totalRes = resQuery.Count();

            var reservations = GetReservations(resQuery
                .Skip((currentPage - 1) * resPerPage)
                .Take(resPerPage));

            foreach (var res in reservations)
            {
                if (res != null)
                {
                    res.Cats = CatsInReservations(res.Id);
                }
            }

            return new AdminQueryReservationServiceModel()
            {
                TotalReservations = totalRes,
                CurrentPage = currentPage,
                ResPerPage = resPerPage,
                Reservations = reservations
            };
        }

        private void FilterReservations()
        {
            var reservations = _data
                .Reservations
                .Where(r => r.IsActive == true)
                .ToList();

            foreach (var res in reservations)
            {
                if (res.Departure < DateTime.UtcNow)
                {
                    res.IsActive = false;
                    _data.SaveChanges();
                }
            }
        }

        private IEnumerable<ResCatServiceModel> CatsInReservations(string resId)
            => _data
                .CatsReservations
                .Where(cr => cr.ReservationId == resId)
                .Select(cr => new ResCatServiceModel()
                {
                    Name = cr.Cat.Name,
                    BreedName = cr.Cat.Breed.Name,
                    PhotoUrl = cr.Cat.PhotoUrl
                })
                .ToList();

        private IEnumerable<ResServiceModel> GetReservations(IQueryable<Reservation> resQuery)
            => resQuery
                .Select(r => new ResServiceModel()
                    {
                        Id = r.Id,
                        DateOfReservation = r.DateOfReservation,
                        Arrival = r.Arrival.ToString("MM/dd/yyyy"),
                        Departure = r.Departure.ToString("MM/dd/yyyy"),
                        RoomTypeName = r.RoomType.Name,
                        TotalPrice = $"${r.Payment.TotalPrice:f2}",
                        IsActive = r.IsActive,
                        IsApproved = r.IsApproved
                    })
                .ToList();
    }
}