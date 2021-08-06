namespace CatHotel.Infrastructure
{
    using Areas.Admin.Models;
    using AutoMapper;
    using Data.Models;
    using Models.Cat.FormModel;
    using Models.Reservation.ViewModels;
    using Services.Models.Cats;
    using Services.Models.Cats.AdminArea;
    using Services.Models.Cats.CommonArea;
    using Services.Models.Reservations;

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<AddCatFormModel, Cat>();
            CreateMap<Cat, CatServiceModel>();
            CreateMap<Breed, CatBreedServiceModel>();
            CreateMap<Cat, AdminCatServiceModel>();
            CreateMap<Cat, AdminCatEditViewModel>();

            CreateMap<ResServiceModel, ResViewModel>();
        }
    }
}