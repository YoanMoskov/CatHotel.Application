namespace CatHotel.Services.CatService
{
    using System.Collections.Generic;
    using Models.Cats;

    public interface ICatService
    {
        string Add(
            string name,
            int age,
            string photoUrl,
            int breedId,
            string additionalInformation,
            string userId);

        List<CatServiceModel> All(string userId);

        bool Edit(
            int age,
            string photoUrl,
            string additionalInformation,
            string catId);

        bool Delete(string catId);

        public CatDetailsServiceModel Details(string catId);

        bool DoesBreedExist(int breedId);

        IEnumerable<CatBreedServiceModel> GetBreeds();
    }
}