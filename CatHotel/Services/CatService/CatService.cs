namespace CatHotel.Services.CatService
{
    using Data;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using System.Collections.Generic;
    using System.Linq;
    using Controllers.ViewModels.Cat;
    using Data.Models;

    public class CatService : ICatService
    {
        private readonly ApplicationDbContext data;

        public CatService(ApplicationDbContext data)
        {
            this.data = data;
        }

        public List<SelectListItem> BreedsAsSelectListItems()
        {
            var result = data.Breeds
                .Select(b => new SelectListItem()
                {
                    Text = b.Name,
                    Value = b.BreedId.ToString()
                })
                .ToList();
            return result;
        }

        public void InsertOptionSelectBreed(List<SelectListItem> breedList)
        {
            breedList.Insert(0, new SelectListItem()
            {
                Text = "Select breed",
                Value = string.Empty
            });
        }

        public void AddCat(CatFormModel c, User user)
        {
            var cat = new Cat()
            {
                Name = c.Name,
                Age = c.Age,
                PhotoUrl = c.PhotoUrl,
                BreedId = c.BreedId,
            };

            user.Cats.Add(cat);
            data.SaveChanges();
        }
    }
}