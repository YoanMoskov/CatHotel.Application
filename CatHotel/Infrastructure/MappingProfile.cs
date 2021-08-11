namespace CatHotel.Infrastructure
{
    using Areas.Admin.Models.Cats;
    using AutoMapper;
    using Data.Models;
    using Models.Cat.FormModel;
    using Models.Reservation.ViewModels;
    using Services.Models.Cats.AdminArea;
    using Services.Models.Cats.CommonArea;
    using Services.Models.Reservations.CommonArea;

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<AddCatFormModel, Cat>();
            CreateMap<Cat, CatServiceModel>();
            CreateMap<Breed, CatBreedServiceModel>();
            CreateMap<Cat, AdminCatServiceModel>();
            CreateMap<Cat, ResCatServiceModel>();
            CreateMap<Cat, AdminCatEditServiceModel>();

            CreateMap<ResServiceModel, ResViewModel>();
            CreateMap<Reservation, ResServiceModel>();
        }
    }
}