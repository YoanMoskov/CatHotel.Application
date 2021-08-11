﻿namespace CatHotel.Services.CatService
{
    using System;
    using Data.Models;
    using Models.Cats.AdminArea;
    using Models.Cats.CommonArea;
    using System.Collections.Generic;
    using Areas.Admin.Models.Enums;
    using Areas.Admin.Models.Enums.Cats;

    public interface ICatService
    {
        string Add(Cat cat, string userId);

        List<CatServiceModel> All(string userId);

        AdminCatQueryServiceModel AdminAll(
            string breed = null,
            int currentPage = 1,
            CatSorting sorting = CatSorting.Newest,
            CatFiltering filtering = CatFiltering.All,
            int catsPerPage = Int32.MaxValue);

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

        public CatServiceModel Get(string catId);

        bool DoesBreedExist(int breedId);

        bool UserHasCats(string UserId);

        IEnumerable<CatBreedServiceModel> GetBreeds();
    }
}