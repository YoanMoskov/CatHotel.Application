namespace CatHotel.Services.CatService
{
    using System.Linq;
    using System.Collections.Generic;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Data;
    using Data.Models;
    using Models.Cats;

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

            _data.Cats.Add(cat);
            _data.SaveChanges();

            return cat.Id;
        }

        public List<CatServiceModel> All(string userId)
            => _data.Cats
                .Where(c => c.UserId == userId && c.IsDeleted == false)
                .ProjectTo<CatServiceModel>(this._mapper.ConfigurationProvider)
                .ToList();

        public bool Edit(int age, string photoUrl, string additionalInformation, string catId)
        {
            var cat = _data.Cats.FirstOrDefault(c => c.Id == catId);

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

        public CatDetailsServiceModel Details(string catId)
            => _data.Cats
                .Where(c => c.Id == catId)
                .ProjectTo<CatDetailsServiceModel>(this._mapper.ConfigurationProvider)
                .FirstOrDefault();

        public bool DoesBreedExist(int breedId)
            => this._data.Breeds.Any(b => b.Id == breedId);

        public IEnumerable<CatBreedServiceModel> GetBreeds()
            => this._data
                .Breeds
                .ProjectTo<CatBreedServiceModel>(this._mapper.ConfigurationProvider)
                .ToList();
    }
}