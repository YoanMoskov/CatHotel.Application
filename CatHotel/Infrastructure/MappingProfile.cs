namespace CatHotel.Infrastructure
{
    using AutoMapper;
    using Data.Models;
    using Models.Cat.FormModel;
    using Models.Cat.ViewModel;
    using Models.Reservation.ViewModels;
    using Services.Models.Cats;
    using Services.Models.Reservations;

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CatDetailsServiceModel, CatViewModel>();
            CreateMap<AddCatFormModel, Cat>();
            CreateMap<Cat, CatServiceModel>();
            CreateMap<Cat, CatDetailsServiceModel>();
            CreateMap<Breed, CatBreedServiceModel>();

            CreateMap<ResServiceModel, ResViewModel>();
        }
    }
}