namespace CatHotel.Services.CatService
{
    using Data;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using System.Collections.Generic;
    using System.Linq;
    using Data.Models;
    using Models.Cat;

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
            };

            user.Cats.Add(cat);
            data.SaveChanges();
        }
    }
}