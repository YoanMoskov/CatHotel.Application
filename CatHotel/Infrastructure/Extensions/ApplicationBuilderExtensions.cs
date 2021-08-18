namespace CatHotel.Infrastructure.Extensions
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Data;
    using Data.Models;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using static Areas.Admin.AdminConstants;
    using static Data.DataConstants.Style;
    using static WebConstants;

    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder PrepareDatabase(
            this IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.CreateScope();
            var services = serviceScope.ServiceProvider;

            MigrateDatabase(services);

            SeedBreeds(services);
            SeedStyles(services);
            SeedRoomTypes(services);
            SeedRoles(services);
            SeedAdmin(services);

            return app;
        }

        private static void MigrateDatabase(IServiceProvider services)
        {
            var data = services.GetRequiredService<ApplicationDbContext>();

            data.Database.Migrate();
        }

        private static void SeedBreeds(IServiceProvider services)
        {
            var data = services.GetRequiredService<ApplicationDbContext>();

            if (data.Breeds.Any())
            {
                return;
            }

            data.Breeds.AddRange(new[]
            {
                new Breed() {Name = "Abyssinian"},
                new Breed() {Name = "American Bob Tail"},
                new Breed() {Name = "American Maine Coon"},
                new Breed() {Name = "American Shorthair"},
                new Breed() {Name = "American Wirehair"},
                new Breed() {Name = "Angora"},
                new Breed() {Name = "Australian Mist"},
                new Breed() {Name = "Balinese"},
                new Breed() {Name = "Bengal"},
                new Breed() {Name = "Birman"},
                new Breed() {Name = "Bombay"},
                new Breed() {Name = "British Shorthair"},
                new Breed() {Name = "Burmese"},
                new Breed() {Name = "Burmilla"},
                new Breed() {Name = "Chartreaux"},
                new Breed() {Name = "Chausie"},
                new Breed() {Name = "Chinchilla"},
                new Breed() {Name = "Cornish Rex"},
                new Breed() {Name = "Devon Rex"},
                new Breed() {Name = "Domestic Long Hair"},
                new Breed() {Name = "Domestic Medium Hair"},
                new Breed() {Name = "Domestic Short Hair"},
                new Breed() {Name = "Egyptian Mau"},
                new Breed() {Name = "Exotic Short Hair"},
                new Breed() {Name = "Foreign White"},
                new Breed() {Name = "Havana"},
                new Breed() {Name = "Himalayan"},
                new Breed() {Name = "Japanese Bobtail"},
                new Breed() {Name = "Korat"},
                new Breed() {Name = "LaPerm Long Hair"},
                new Breed() {Name = "LaPerm Short Hair"},
                new Breed() {Name = "Maine Coon"},
                new Breed() {Name = "Manx"},
                new Breed() {Name = "Munchkin"},
                new Breed() {Name = "Norwegian Forest Cat"},
                new Breed() {Name = "Other"}
            });

            data.SaveChanges();
        }

        private static void SeedStyles(IServiceProvider services)
        {
            var data = services.GetRequiredService<ApplicationDbContext>();

            if (data.Styles.Any())
            {
                return;
            }

            data.Styles.AddRange(new[]
            {
                new Style() {Description = NaturalStyleDesc, Name = NaturalStyleName, Price = NaturalStylePrice, PhotoUrl = NaturalPhotoUrl},
                new Style() {Description = TigonStyleDesc, Name = TigonStyleName, Price = TigonStylePrice, PhotoUrl = TigonPhotoUrl},
                new Style() {Description = LionStyleDesc, Name = LionStyleName, Price = LionStylePrice, PhotoUrl = LionPhotoUrl}
            });

            data.SaveChanges();
        }

        private static void SeedRoomTypes(IServiceProvider services)
        {
            var data = services.GetRequiredService<ApplicationDbContext>();

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

        private static void SeedAdmin(IServiceProvider services)
        {
            var userManager = services.GetRequiredService<UserManager<User>>();

            Task
                .Run(async () =>
                {
                    const string adminEmail = "admin@ch.com";
                    const string adminPassword = "catHotel12";

                    var user = new User
                    {
                        Email = adminEmail,
                        UserName = adminEmail,
                        FirstName = "Admin",
                        LastName = "Admin",
                    };

                    await userManager.CreateAsync(user, adminPassword);

                    await userManager.AddToRoleAsync(user, AdminRoleName);
                })
                .GetAwaiter()
                .GetResult();
        }

        private static void SeedRoles(IServiceProvider services)
        {
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

            Task
                .Run(async () =>
                {
                    if (!await roleManager.RoleExistsAsync(AdminRoleName))
                    {
                        await roleManager.CreateAsync(new IdentityRole(AdminRoleName));
                    }

                    if (!await roleManager.RoleExistsAsync(UserRoleName))
                    {
                        await roleManager.CreateAsync(new IdentityRole(UserRoleName));
                    }
                })
                .GetAwaiter()
                .GetResult();
        }
    }
}