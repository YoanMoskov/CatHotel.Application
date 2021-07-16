namespace CatHotel.Services.CatService
{
    using System.Collections.Generic;
    using Data.Models;
    using Models.Cat;
    using Models.Cat.FormModel;
    using Models.Cat.ViewModel;

    public interface ICatService
    {
        void AddCat(AddCatFormModel c, User user);

        List<CatViewModel> GetAllCatsCatViewModels(string userId);

        CatViewModel GetCatInViewModel(string catId);

        void EditCat(EditCatFormModel c, string catId);

        void DeleteCat(string catId);

        Cat GetCatById(string catId);
    }
}