namespace CatHotel.Services.CatService
{
    using System.Collections.Generic;
    using Areas.Admin.Models.Enums.Cats;
    using Models.Cats.AdminArea;
    using Models.Cats.CommonArea;

    public interface ICatService
    {
        string Add(CatServiceModel cat, string userId);

        List<CatServiceModel> All(string userId);

        AdminCatQueryServiceModel AdminAll(
            string breed = null,
            int currentPage = 1,
            CatSorting sorting = CatSorting.Newest,
            CatFiltering filtering = CatFiltering.All,
            int catsPerPage = int.MaxValue);

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

        CatServiceModel Get(string catId);

        AdminCatEditServiceModel AdminGet(string catId);

        bool DoesBreedExist(int breedId);

        bool DoesCatExist(string catId);

        bool UserHasCats(string UserId);

        IEnumerable<CatBreedServiceModel> GetBreeds();
    }
}