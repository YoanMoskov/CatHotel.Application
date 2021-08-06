namespace CatHotel.Services.UserService
{
    using Data;
    using System.Linq;

    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _data;

        public UserService(ApplicationDbContext data)
        {
            this._data = data;
        }

        public bool UserHasCats(string userId)
            => _data.Users
                .FirstOrDefault(u => u.Id == userId).Cats.Any();
    }
}