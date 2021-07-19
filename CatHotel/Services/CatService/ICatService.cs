namespace CatHotel.Services.CatService
{
    using Data.Models;
    using Models.Cat.FormModel;
    using Models.Cat.ViewModel;
    using System.Collections.Generic;

    public interface ICatService
    {
        void AddCat(AddCatFormModel c, User user);

        List<CatViewModel> GetAllCatsCatViewModels(string userId);

        CatViewModel GetCatInViewModel(string catId);

        void EditCat(EditCatFormModel c, string catId);

        void DeleteCat(string catId);

        IEnumerable<CatBreedViewModel> GetCatBreeds();

        Cat GetCatById(string catId);
    }
}