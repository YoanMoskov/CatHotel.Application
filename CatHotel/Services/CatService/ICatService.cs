namespace CatHotel.Services.CatService
{
    using System.Collections.Generic;
    using Controllers.ViewModels.Cat;
    using Data.Models;
    using Microsoft.AspNetCore.Mvc.Rendering;

    public interface ICatService
    {
        List<SelectListItem> BreedsAsSelectListItems();

        void InsertOptionSelectBreed(List<SelectListItem> breedList);

        void AddCat(CatFormModel c, User user);
    }
}