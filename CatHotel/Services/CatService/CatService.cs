namespace CatHotel.Services.CatService
{
    using System.Linq;
    using System.Collections.Generic;
    using Data;
    using Data.Models;
    using Models.Cats;

    public class CatService : ICatService
    {
        private readonly ApplicationDbContext data;

        public CatService(ApplicationDbContext data)
        {
            this.data = data;
        }

        public string Add(string name, int age, string photoUrl, int breedId, string additionalInformation, string userId)
        {
            var cat = new Cat()
            {
                Name = name,
                Age = age,
                PhotoUrl = photoUrl,
                BreedId = breedId,
                AdditionalInformation = additionalInformation,
                UserId = userId
            };

            data.Cats.Add(cat);
            data.SaveChanges();

            return cat.Id;
        }

        public List<CatServiceModel> All(string userId)
            => data.Cats
                .Where(c => c.UserId == userId && c.IsDeleted == false)
                .Select(c => new CatServiceModel()
                {
                    Id = c.Id,
                    Name = c.Name,
                    Age = c.Age,
                    PhotoUrl = c.PhotoUrl,
                    BreedName = c.Breed.Name
                }).ToList();

        public bool Edit(int age, string photoUrl, string additionalInformation, string catId)
        {
            var cat = data.Cats.FirstOrDefault(c => c.Id == catId);

            if (cat == null)
            {
                return false;
            }

            cat.Age = age;
            cat.PhotoUrl = photoUrl;
            cat.AdditionalInformation = additionalInformation;

            data.SaveChanges();

            return true;
        }

        public bool Delete(string catId)
        {
            var cat = data.Cats.Find(catId);
            if (cat == null)
            {
                return false;
            }

            cat.IsDeleted = true;

            data.SaveChanges();

            return true;
        }

        public CatDetailsServiceModel Details(string catId)
            => data.Cats
                .Where(c => c.Id == catId)
                .Select(c => new CatDetailsServiceModel()
                {
                    Id = c.Id,
                    Name = c.Name,
                    Age = c.Age,
                    PhotoUrl = c.PhotoUrl,
                    AdditionalInformation = c.AdditionalInformation,
                    BreedName = c.Breed.Name
                })
                .FirstOrDefault();

        public bool DoesBreedExist(int breedId)
            => this.data.Breeds.Any(b => b.Id == breedId);

        public IEnumerable<CatBreedServiceModel> GetBreeds()
            => this.data
                .Breeds
                .Select(c => new CatBreedServiceModel()
                {
                    Id = c.Id,
                    Name = c.Name
                })
                .ToList();
    }
}