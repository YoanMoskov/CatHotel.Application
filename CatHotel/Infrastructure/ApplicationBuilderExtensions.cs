namespace CatHotel.Infrastructure
{
    using Data;
    using Data.Models;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using System.Linq;

    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder PrepareDatabase(
            this IApplicationBuilder app)
        {
            using var scopedServices = app.ApplicationServices.CreateScope();

            var data = scopedServices.ServiceProvider.GetService<ApplicationDbContext>();

            data.Database.Migrate();
            SeedBreeds(data);
            SeedRoomTypes(data);

            return app;
        }

        private static void SeedBreeds(ApplicationDbContext data)
        {
            if (data.Breeds.Any())
            {
                return;
            }

            data.Breeds.AddRange(new[]
            {
                new Breed() {Name = "Special"},
                new Breed() {Name = "Very special"}
            });

            data.SaveChanges();
        }

        private static void SeedRoomTypes(ApplicationDbContext data)
        {
            if (data.RoomTypes.Any())
            {
                return;
            }

            data.RoomTypes.AddRange(new[]
            {
                new RoomType() {Name = "Luxury", Description = "Cool", PricePerDay = 100.00m},
                new RoomType() {Name = "Standard", Description = "Not so cool", PricePerDay = 50.00m}
            });

            data.SaveChanges();
        }
    }
}