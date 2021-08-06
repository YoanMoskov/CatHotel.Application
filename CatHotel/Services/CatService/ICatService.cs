namespace CatHotel.Services.CatService
{
    using Data.Models;
    using Models.Cats.AdminArea;
    using Models.Cats.CommonArea;
    using System.Collections.Generic;

    public interface ICatService
    {
        string Add(Cat cat, string userId);

        List<CatServiceModel> All(string userId);

        List<AdminCatServiceModel> AdminAll();

        bool Edit(
            int age,
            string photoUrl,
            string additionalInformation,
            string catId);

        bool AdminEdit(
            string name,
            int age,
            string photoUrl,
            int breedId,
            string additionalInformation,
            string catId);

        bool Delete(string catId);

        bool AdminRestore(string catId);

        public CatServiceModel Cat(string catId);

        bool DoesBreedExist(int breedId);

        IEnumerable<CatBreedServiceModel> GetBreeds();

        Cat FindCatById(string catId);
    }
}