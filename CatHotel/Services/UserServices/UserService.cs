namespace CatHotel.Services.UserServices
{
    using System.Linq;
    using System.Security.Claims;
    using Data;
    using Data.Models;

    public class UserService : IUserService
    {
        private readonly ApplicationDbContext data;

        public UserService(ApplicationDbContext data)
        {
            this.data = data;
        }

        public User CurrentlyLoggedUser(ClaimsPrincipal user)
            =>  data.Users
            .FirstOrDefault(u => u.Id == user.FindFirstValue(ClaimTypes.NameIdentifier));

        public bool UserHasCats(string userId)
            => data.Users
                .FirstOrDefault(u => u.Id == userId).Cats.Any();
    }
}