namespace CatHotel.Services.CatService
{
    using System.Linq;
    using System.Collections.Generic;
    using Data;
    using Data.Models;
    using Models.Cat.FormModel;
    using Models.Cat.ViewModel;

    public class CatService : ICatService
    {
        private readonly ApplicationDbContext data;

        public CatService(ApplicationDbContext data)
        {
            this.data = data;
        }

        public void AddCat(AddCatFormModel c, User user)
        {
            var cat = new Cat()
            {
                Name = c.Name,
                Age = c.Age,
                PhotoUrl = c.PhotoUrl,
                BreedId = c.BreedId,
                AdditionalInformation = c.AdditionalInformation
            };

            user.Cats.Add(cat);
            data.SaveChanges();
        }

        public void EditCat(EditCatFormModel c, string catId)
        {
            var cat = data.Cats.FirstOrDefault(c => c.Id == catId);
            cat.Age = c.Age;
            cat.PhotoUrl = c.PhotoUrl;
            cat.AdditionalInformation = c.AdditionalInformation;

            data.SaveChanges();
        }

        public void DeleteCat(string catId)
        {
            data.Cats
                .FirstOrDefault(c => c.Id == catId).IsDeleted = true;

            data.SaveChanges();
        }

        public bool DoesBreedExist(int breedId)
            => this.data.Breeds.Any(b => b.Id == breedId);

            public IEnumerable<CatBreedViewModel> GetCatBreeds()
            => this.data
                .Breeds
                .Select(c => new CatBreedViewModel()
                {
                    Id = c.Id,
                    Name = c.Name
                })
                .ToList();

        public CatViewModel GetCatInViewModel(string catId)
        => data.Cats
            .Where(c => c.Id == catId)
            .Select(c => new CatViewModel()
            {
                Id = c.Id,
                Name = c.Name,
                Age = c.Age,
                PhotoUrl = c.PhotoUrl,
                AdditionalInformation = c.AdditionalInformation,
                Breed = new CatBreedViewModel()
                {
                    Id = c.BreedId,
                    Name = c.Breed.Name
                }
            })
            .FirstOrDefault();

        public List<CatViewModel> GetAllCatsCatViewModels(string userId)
            => data.Cats
                .Where(c => c.UserId == userId && c.IsDeleted == false)
                .Select(c => new CatViewModel()
                {
                    Id = c.Id,
                    Name = c.Name,
                    Age = c.Age,
                    PhotoUrl = c.PhotoUrl,
                    Breed = new CatBreedViewModel()
                    {
                        Name = c.Breed.Name
                    }
                }).ToList();
    }
}