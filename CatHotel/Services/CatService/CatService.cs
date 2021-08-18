namespace CatHotel.Services.CatService
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Areas.Admin.Models.Enums.Cats;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Data;
    using Data.Models;
    using Models.Cats.AdminArea;
    using Models.Cats.CommonArea;

    public class CatService : ICatService
    {
        private readonly ApplicationDbContext _data;
        private readonly IMapper _mapper;

        public CatService(
            IMapper mapper,
            ApplicationDbContext data)
        {
            _mapper = mapper;
            _data = data;
        }

        public string Add(CatServiceModel cat, string userId)
        {
            var newCat = new Cat
            {
                Name = cat.Name,
                Age = cat.Age,
                PhotoUrl = cat.PhotoUrl,
                DateAdded = DateTime.UtcNow,
                AdditionalInformation = cat.AdditionalInformation,
                BreedId = cat.BreedId,
                UserId = userId
            };

            _data.Cats.Add(newCat);
            _data.SaveChanges();

            return cat.Id;
        }

        public List<CatServiceModel> All(string userId)
            => _data.Cats
                .Where(c => c.UserId == userId && c.IsDeleted == false)
                .ProjectTo<CatServiceModel>(_mapper.ConfigurationProvider)
                .ToList();

        public AdminCatQueryServiceModel AdminAll(
            string breed = null,
            int currentPage = 1,
            CatSorting sorting = CatSorting.Newest,
            CatFiltering filtering = CatFiltering.All,
            int catsPerPage = int.MaxValue)
        {
            IQueryable<Cat> catsQuery = _data.Cats;

            if (!string.IsNullOrWhiteSpace(breed)) catsQuery = catsQuery.Where(c => c.Breed.Id == int.Parse(breed));

            catsQuery = filtering switch
            {
                CatFiltering.Available => catsQuery.Where(c => c.IsDeleted == false),
                CatFiltering.Deleted => catsQuery.Where(c => c.IsDeleted),
                CatFiltering.All or _ => catsQuery
            };

            catsQuery = sorting switch
            {
                CatSorting.Oldest => catsQuery.OrderBy(c => c.DateAdded),
                CatSorting.Newest or _ => catsQuery.OrderByDescending(c => c.DateAdded)
            };

            var totalCats = catsQuery.Count();

            var cats = GetCats(catsQuery
                .Skip((currentPage - 1) * catsPerPage)
                .Take(catsPerPage));

            return new AdminCatQueryServiceModel
            {
                TotalCats = totalCats,
                CurrentPage = currentPage,
                CatsPerPage = catsPerPage,
                Cats = cats
            };
        }

        public bool Edit(int age, string photoUrl, string additionalInformation, string catId)
        {
            var cat = FindCatById(catId);

            cat.Age = age;
            cat.PhotoUrl = photoUrl;
            cat.AdditionalInformation = additionalInformation;

            _data.SaveChanges();

            return true;
        }

        public bool AdminEdit(string name, int age, string photoUrl, int breedId, string additionalInformation,
            string catId)
        {
            var cat = FindCatById(catId);

            cat.Name = name;
            cat.Age = age;
            cat.PhotoUrl = photoUrl;
            cat.BreedId = breedId;
            cat.AdditionalInformation = additionalInformation;

            _data.SaveChanges();

            return true;
        }

        public bool AdminRestore(string catId)
        {
            var cat = _data.Cats.Find(catId);

            if (cat == null) return false;

            cat.IsDeleted = false;

            _data.SaveChanges();

            return true;
        }

        public bool Delete(string catId)
        {
            var cat = _data.Cats.Find(catId);
            if (cat == null) return false;

            cat.IsDeleted = true;

            _data.SaveChanges();

            return true;
        }

        public CatServiceModel Get(string catId)
            => _data.Cats
                .Where(c => c.Id == catId)
                .ProjectTo<CatServiceModel>(_mapper.ConfigurationProvider)
                .FirstOrDefault();

        public AdminCatEditServiceModel AdminGet(string catId)
            => _data.Cats
                .Where(c => c.Id == catId)
                .ProjectTo<AdminCatEditServiceModel>(_mapper.ConfigurationProvider)
                .FirstOrDefault();

        public bool DoesBreedExist(int breedId)
            => _data.Breeds.Any(b => b.Id == breedId);

        public bool DoesCatExist(string catId)
            => _data.Cats
                .Any(c => c.Id == catId);

        public bool UserHasCats(string UserId)
            => _data.Cats.Any(c => c.UserId == UserId);

        public IEnumerable<CatBreedServiceModel> GetBreeds()
            => _data
                .Breeds
                .ProjectTo<CatBreedServiceModel>(_mapper.ConfigurationProvider)
                .ToList();

        private Cat FindCatById(string catId)
            => _data.Cats.FirstOrDefault(c => c.Id == catId);

        private IEnumerable<AdminCatServiceModel> GetCats(IQueryable<Cat> catQuery)
            => catQuery
                .ProjectTo<AdminCatServiceModel>(_mapper.ConfigurationProvider)
                .ToList();
    }
}