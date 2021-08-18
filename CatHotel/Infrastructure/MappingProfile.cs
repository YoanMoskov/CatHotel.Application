namespace CatHotel.Infrastructure
{
    using AutoMapper;
    using Data.Models;
    using Models.Cat.FormModel;
    using Models.Reservation.ViewModels;
    using Services.Models.Cats.AdminArea;
    using Services.Models.Cats.CommonArea;
    using Services.Models.Groomings.CommonArea;
    using Services.Models.Reservations.CommonArea;

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Cat, CatServiceModel>();
            CreateMap<Breed, CatBreedServiceModel>();
            CreateMap<Cat, AdminCatServiceModel>();
            CreateMap<Cat, ResCatServiceModel>();
            CreateMap<Cat, AdminCatEditServiceModel>();
            CreateMap<AddCatFormModel, CatServiceModel>();
            CreateMap<CatServiceModel, Cat>();

            CreateMap<ResServiceModel, ResViewModel>();
            CreateMap<Reservation, ResServiceModel>();

            CreateMap<Style, GroomingStyleServiceModel>();
            CreateMap<Cat, GroomingCatServiceModel>();
        }
    }
}