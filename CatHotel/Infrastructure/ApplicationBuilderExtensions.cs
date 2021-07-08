namespace CatHotel.Infrastructure
{
    using System.Linq;
    using Data;
    using Data.Models;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;

    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder PrepareDatabase(
            this IApplicationBuilder app)
        {
            using var scopedServices = app.ApplicationServices.CreateScope();

            var data = scopedServices.ServiceProvider.GetService<ApplicationDbContext>();

            data.Database.Migrate();

            return app;
        }

        private static void SeedCategories(ApplicationDbContext data)
        {
            if (data.Breeds.Any())
            {
                return;
            }

            data.Breeds.AddRange(new []
            {
                new Breed() {Name = "Special"},
                new Breed() {Name = "Very special"}
            });
        }
    }
}