namespace CatHotel.Services.CatService
{
    using System.Collections.Generic;
    using Data.Models;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Models.Cat;

    public interface ICatService
    {
        void AddCat(AddCatFormModel c, User user);
    }
}