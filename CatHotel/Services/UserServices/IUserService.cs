namespace CatHotel.Services.UserServices
{
    using System.Security.Claims;
    using Data.Models;

    public interface IUserService
    {
        User CurrentlyLoggedUser(ClaimsPrincipal user);

        bool UserHasCats(string userId);
    }
}