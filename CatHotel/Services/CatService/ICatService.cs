namespace CatHotel.Services.CatService
{
    using Data.Models;
    using Models.Cat;

    public interface ICatService
    {
        void AddCat(AddCatFormModel c, User user);
    }
}