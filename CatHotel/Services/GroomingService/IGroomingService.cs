namespace CatHotel.Services.GroomingService
{
    using System;
    using System.Collections.Generic;
    using Areas.Admin.Models.Enums.Groomings;
    using Models.Groomings.AdminArea;
    using Models.Groomings.CommonArea;

    public interface IGroomingService
    {
        void Create(string catId, int styleId, DateTime appointment, string userId);

        IEnumerable<GroomingServiceModel> All(string userId);

        AdminQueryGroomingServiceModel AdminAll(
            string breedId = null,
            int currentPage = 1,
            GroomsSorting sorting = GroomsSorting.Newest,
            GroomsFiltering filtering = GroomsFiltering.PendingApproval,
            int groomsPerPage = Int32.MaxValue);

        public bool AdminApprove(string groomId);

        IEnumerable<GroomingCatServiceModel> GetCatsOfUser(string userId, int styleId);

        bool DoesStyleExist(int styleId);

        IEnumerable<GroomingStyleServiceModel> GetStyles();

        GroomingStyleServiceModel SelectedStyle(int styleId);

        GroomingCatServiceModel SelectedCat(string catId);
    }
}