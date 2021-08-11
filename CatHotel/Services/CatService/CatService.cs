namespace CatHotel.Services.CatService
{
    using Areas.Admin.Models.Enums.Cats;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Data;
    using Data.Models;
    using Models.Cats.AdminArea;
    using Models.Cats.CommonArea;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class CatService : ICatService
    {
        private readonly ApplicationDbContext _data;
        private readonly IMapper _mapper;

        public CatService(
            IMapper mapper,
            ApplicationDbContext data)
        {
            this._mapper = mapper;
            this._data = data;
        }

        public string Add(Cat cat, string userId)
        {
            cat.UserId = userId;
            cat.DateAdded = DateTime.UtcNow;

            _data.Cats.Add(cat);
            _data.SaveChanges();

            return cat.Id;
        }

        public List<CatServiceModel> All(string userId)
            => _data.Cats
                .Where(c => c.UserId == userId && c.IsDeleted == false)
                .ProjectTo<CatServiceModel>(this._mapper.ConfigurationProvider)
                .ToList();

        public AdminCatQueryServiceModel AdminAll(
            string breed = null,
            int currentPage = 1,
            CatSorting sorting = CatSorting.Newest,
            CatFiltering filtering = CatFiltering.All,
            int catsPerPage = Int32.MaxValue)
        {
            IQueryable<Cat> catsQuery = _data.Cats;

            if (!string.IsNullOrWhiteSpace(breed))
            {
                catsQuery = catsQuery.Where(c => c.Breed.Id == int.Parse(breed));
            }

            catsQuery = filtering switch
            {
                CatFiltering.Available => catsQuery.Where(c => c.IsDeleted == false),
                CatFiltering.Deleted => catsQuery.Where(c => c.IsDeleted == true),
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

            return new AdminCatQueryServiceModel()
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

            if (cat == null)
            {
                return false;
            }

            cat.Age = age;
            cat.PhotoUrl = photoUrl;
            cat.AdditionalInformation = additionalInformation;

            _data.SaveChanges();

            return true;
        }

        public bool AdminEdit(string name, int age, string photoUrl, int breedId, string additionalInformation, string catId)
        {
            var cat = FindCatById(catId);

            if (cat == null)
            {
                return false;
            }

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
            if (cat == null)
            {
                return false;
            }

            cat.IsDeleted = false;

            _data.SaveChanges();

            return true;
        }

        public bool Delete(string catId)
        {
            var cat = _data.Cats.Find(catId);
            if (cat == null)
            {
                return false;
            }

            cat.IsDeleted = true;

            _data.SaveChanges();

            return true;
        }

        public CatServiceModel Get(string catId)
            => _data.Cats
                .Where(c => c.Id == catId)
                .ProjectTo<CatServiceModel>(this._mapper.ConfigurationProvider)
                .FirstOrDefault();

        public bool DoesBreedExist(int breedId)
            => this._data.Breeds.Any(b => b.Id == breedId);

        public bool UserHasCats(string UserId)
            => _data.Cats.Any(c => c.UserId == UserId);

        public IEnumerable<CatBreedServiceModel> GetBreeds()
            => this._data
                .Breeds
                .ProjectTo<CatBreedServiceModel>(this._mapper.ConfigurationProvider)
                .ToList();

        private Cat FindCatById(string catId)
            => _data.Cats.FirstOrDefault(c => c.Id == catId);

        private IEnumerable<AdminCatServiceModel> GetCats(IQueryable<Cat> catQuery)
            => catQuery
                .ProjectTo<AdminCatServiceModel>(_mapper.ConfigurationProvider)
                .ToList();
    }
}