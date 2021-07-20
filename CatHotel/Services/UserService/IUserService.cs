﻿namespace CatHotel.Services.UserService
{
    using System.Security.Claims;
    using Data.Models;

    public interface IUserService
    {
        User CurrentlyLoggedUser(ClaimsPrincipal user);

        bool UserHasCats(string userId);
    }
}