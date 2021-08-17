namespace CatHotel.Services.ReservationService
{
    using Areas.Admin.Models.Enums.Reservations;
    using Data;
    using Data.Models;
    using Data.Models.Enums;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Models.Reservations.AdminArea;
    using Models.Reservations.CommonArea;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class ReservationService : IReservationService
    {
        private readonly ApplicationDbContext _data;

        private const ReservationState Pending = ReservationState.Pending;
        private const ReservationState Active = ReservationState.Active;
        private const ReservationState Expired = ReservationState.Expired;

        public ReservationService(ApplicationDbContext data)
        {
            this._data = data;
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
                ReservationState = Pending,
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

        public string AreCatsInResTimeFrame(string[] catIds, DateTime arrival, DateTime departure)
        {
            var errorMessage = new StringBuilder();
            var cats = new List<ResActiveCatServiceModel>();
            foreach (var catId in catIds)
            {
                var reservations = _data.CatsReservations
                    .Where(cr => cr.CatId == catId && cr.Reservation.ReservationState != Expired)
                    .Select(r => new ResActiveServiceModel()
                    {
                        Arrival = r.Reservation.Arrival,
                        Departure = r.Reservation.Departure
                    });
                foreach (var res in reservations)
                {
                    if (!(res.Arrival < arrival && res.Departure < departure && res.Departure < arrival && res.Arrival < departure || res.Arrival > arrival && res.Departure > departure && res.Departure > arrival && res.Arrival > departure))
                    {
                        cats.Add(GetCatInActiveCatServiceModel(catId));
                    }
                }
            }

            if (cats.Count == 1)
            {
                errorMessage.AppendLine($"The cat: {cats.First().Name} - {cats.First().BreedName} is already in a reservation in this time frame.");
            }
            else if (cats.Count > 1)
            {
                errorMessage.AppendLine("The cats: ");
                foreach (var cat in cats)
                {
                    errorMessage.Append($"{cat.Name} - {cat.BreedName}, ");
                }

                errorMessage.Append("are already in reservation in this time frame.");
            }

            return errorMessage.ToString();
        }

        public IEnumerable<ResRoomTypeServiceModel> RoomTypes()
            => this._data
                .RoomTypes
                .Select(rt => new ResRoomTypeServiceModel()
                {
                    Id = rt.Id,
                    Name = rt.Name,
                    PricePerDay = rt.PricePerDay
                })
                .ToList();

        public IEnumerable<ResServiceModel> AllWithState(string userId, ReservationState resState, bool isApproved)
        {
            FilterReservations(false, userId);

            var reservations = _data
                .Reservations
                .Where(r => r.UserId == userId && r.ReservationState == resState && r.IsApproved == isApproved)
                .Select(r => new ResServiceModel()
                {
                    Id = r.Id,
                    DateOfReservation = r.DateOfReservation,
                    Arrival = r.Arrival.ToString("MM/dd/yyyy"),
                    Departure = r.Departure.ToString("MM/dd/yyyy"),
                    RoomTypeName = r.RoomType.Name,
                    TotalPrice = $"${r.Payment.TotalPrice:f2}",
                    ReservationState = r.ReservationState,
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
                ResFiltering.Active => resQuery.Where(r => r.ReservationState == Active),
                ResFiltering.Expired => resQuery.Where(r => r.ReservationState == Expired),
                ResFiltering.Pending => resQuery.Where(r => r.ReservationState == Pending),
                ResFiltering.PendingApproval or _ => resQuery.Where(r => r.IsApproved == false && r.ReservationState == Pending)
            };

            resQuery = sorting switch
            {
                ResSorting.Oldest => resQuery.OrderBy(r => r.DateOfReservation),
                ResSorting.Newest or _ => resQuery.OrderByDescending(r => r.DateOfReservation)
            };

            var totalRes = resQuery.Count();

            FilterReservations(true);

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

        public bool AdminApprove(string resId)
        {
            var res = _data.Reservations
                .FirstOrDefault(r => r.Id == resId && r.IsApproved == false);

            if (res != null)
            {
                res.IsApproved = true;

                _data.SaveChanges();

                return true;
            }

            return false;
        }

        public bool DoesCatExist(string catId)
            => _data.Cats.Any(c => c.Id == catId);

        public bool DoesRoomTypeExist(int roomTypeId)
            => _data.RoomTypes.Any(rt => rt.Id == roomTypeId);

        private void FilterReservations(bool isAdmin, string userId = null)
        {
            var reservations = new List<Reservation>();
            if (!isAdmin)
            {
                reservations = _data
                    .Reservations
                    .Where(r => r.UserId == userId && r.ReservationState != Expired)
                    .ToList();
            }
            else
            {
                reservations = _data.Reservations
                    .Where(r => r.ReservationState == Pending || r.ReservationState == Active)
                    .ToList();
            }

            foreach (var res in reservations)
            {
                if (res.Arrival < DateTime.UtcNow && res.Departure > DateTime.UtcNow && res.ReservationState != Active && res.IsApproved)
                {
                    res.ReservationState = Active;

                    _data.SaveChanges();
                }
                else if (res.Departure < DateTime.UtcNow)
                {
                    res.ReservationState = Expired;

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
                    ReservationState = r.ReservationState,
                    IsApproved = r.IsApproved
                })
                .ToList();

        private ResActiveCatServiceModel GetCatInActiveCatServiceModel(string catId)
            => _data.Cats
                .Where(c => c.Id == catId)
                .Select(c => new ResActiveCatServiceModel()
                {
                    Name = c.Name,
                    BreedName = c.Breed.Name
                })
                .FirstOrDefault();
    }
}