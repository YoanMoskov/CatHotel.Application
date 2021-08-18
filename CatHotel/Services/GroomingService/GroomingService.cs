namespace CatHotel.Services.GroomingService
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Areas.Admin.Models.Enums.Groomings;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Data;
    using Data.Models;
    using Models.Groomings.AdminArea;
    using Models.Groomings.CommonArea;

    public class GroomingService : IGroomingService
    {
        private readonly ApplicationDbContext _data;
        private readonly IMapper _mapper;

        public GroomingService(ApplicationDbContext data, IMapper mapper)
        {
            _data = data;
            _mapper = mapper;
        }

        public void Create(string catId, int styleId, DateTime appointment, string userId)
        {
            var grooming = new Grooming
            {
                StyleId = styleId,
                Appointment = appointment,
                UserId = userId,
                CatId = catId,
                DateOfCreation = DateTime.UtcNow
            };

            _data.Groomings.Add(grooming);
            _data.SaveChanges();
        }

        public IEnumerable<GroomingServiceModel> All(string userId)
        {
            FilterGroomings(false, userId);

            var groomings = _data.Groomings
                .Where(g => g.IsExpired == false && g.UserId == userId)
                .Select(g => new GroomingServiceModel
                {
                    CatName = g.Cat.Name,
                    StyleName = g.Style.Name,
                    BreedName = g.Cat.Breed.Name,
                    Appointment = g.Appointment.ToString("MM/dd/yyyy"),
                    IsApproved = g.IsApproved
                })
                .ToList();

            return groomings;
        }

        public AdminQueryGroomingServiceModel AdminAll(
            string styleId = null,
            int currentPage = 1,
            GroomsSorting sorting = GroomsSorting.Newest,
            GroomsFiltering filtering = GroomsFiltering.PendingApproval,
            int groomsPerPage = int.MaxValue)
        {
            IQueryable<Grooming> groomsQuery = _data.Groomings;

            if (!string.IsNullOrWhiteSpace(styleId))
                groomsQuery = groomsQuery.Where(g => g.StyleId == int.Parse(styleId));

            groomsQuery = filtering switch
            {
                GroomsFiltering.Approved => groomsQuery.Where(g => g.IsApproved),
                GroomsFiltering.PendingApproval or _ => groomsQuery.Where(g => g.IsApproved == false)
            };

            groomsQuery = sorting switch
            {
                GroomsSorting.Oldest => groomsQuery.OrderBy(g => g.DateOfCreation),
                GroomsSorting.Newest or _ => groomsQuery.OrderByDescending(g => g.DateOfCreation)
            };

            var totalGrooms = groomsQuery.Count();

            FilterGroomings(true);

            var groomings = GetGroomings(groomsQuery
                .Skip((currentPage - 1) * groomsPerPage)
                .Take(groomsPerPage));


            return new AdminQueryGroomingServiceModel
            {
                TotalGroomings = totalGrooms,
                CurrentPage = currentPage,
                GroomsPerPage = groomsPerPage,
                Groomings = groomings
            };
        }

        public bool AdminApprove(string groomId)
        {
            var groom = _data.Groomings
                .FirstOrDefault(g => g.Id == groomId && g.IsApproved == false);

            if (groom != null)
            {
                groom.IsApproved = true;

                _data.SaveChanges();

                return true;
            }

            return false;
        }

        public IEnumerable<GroomingCatServiceModel> GetCatsOfUser(string userId, int styleId)
            => _data.Cats
                .Where(c => c.UserId == userId)
                .Select(c => new GroomingCatServiceModel
                {
                    Id = c.Id,
                    Name = c.Name,
                    BreedName = c.Breed.Name,
                    PhotoUrl = c.PhotoUrl,
                    StyleId = styleId
                })
                .ToList();

        public bool DoesStyleExist(int styleId)
        {
            return _data.Styles
                .Any(s => s.Id == styleId);
        }

        public IEnumerable<GroomingStyleServiceModel> GetStyles() =>
            _data.Styles
                .ProjectTo<GroomingStyleServiceModel>(_mapper.ConfigurationProvider)
                .ToList();

        public GroomingStyleServiceModel SelectedStyle(int styleId)
        {
            return _data.Styles
                .Where(s => s.Id == styleId)
                .ProjectTo<GroomingStyleServiceModel>(_mapper.ConfigurationProvider)
                .FirstOrDefault();
        }

        public GroomingCatServiceModel SelectedCat(string catId)
        {
            return _data.Cats
                .Where(c => c.Id == catId)
                .Select(c => new GroomingCatServiceModel
                {
                    Id = c.Id,
                    Name = c.Name,
                    BreedName = c.Breed.Name,
                    PhotoUrl = c.PhotoUrl
                })
                .FirstOrDefault();
        }

        private IEnumerable<GroomingServiceModel> GetGroomings(IQueryable<Grooming> groomsQuery)
            => groomsQuery
                .Select(g => new GroomingServiceModel
                {
                    Id = g.Id,
                    CatName = g.Cat.Name,
                    BreedName = g.Cat.Breed.Name,
                    StyleName = g.Style.Name,
                    Appointment = g.Appointment.ToString("MM/dd/yyyy"),
                    IsApproved = g.IsApproved
                })
                .ToList();

        private void FilterGroomings(bool isAdmin, string userId = null)
        {
            var groomings = new List<Grooming>();
            if (!isAdmin)
                groomings = _data
                    .Groomings
                    .Where(g => g.UserId == userId && g.IsExpired == false)
                    .ToList();
            else
                groomings = _data.Groomings
                    .Where(g => g.IsExpired == false)
                    .ToList();

            foreach (var g in groomings)
                if (g.Appointment < DateTime.UtcNow)
                {
                    g.IsExpired = true;

                    _data.SaveChanges();
                }
        }
    }
}