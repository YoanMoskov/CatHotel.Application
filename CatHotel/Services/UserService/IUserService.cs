namespace CatHotel.Services.UserService
{
    using System.Security.Claims;
    using Data.Models;

    public interface IUserService
    {
        bool UserHasCats(string userId);
    }
}