﻿namespace CatHotel.Services.UserService
{
    using Data;
    using Data.Models;
    using System.Linq;
    using System.Security.Claims;

    public class UserService : IUserService
    {
        private readonly ApplicationDbContext data;

        public UserService(ApplicationDbContext data)
        {
            this.data = data;
        }

        public bool UserHasCats(string userId)
            => data.Users
                .FirstOrDefault(u => u.Id == userId).Cats.Any();
    }
}