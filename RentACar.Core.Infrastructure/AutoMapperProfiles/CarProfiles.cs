using AutoMapper;
using RentACar.Data.Models;
using RentACar.DTO.Car;
using RentACar.DTO.Reservation;
using RentACar.Web.ViewModels.Admin;
using RentACar.Web.ViewModels.Car;

namespace RentACar.Core.Infrastructure.AutoMapperProfiles
{
    public class CarProfiles : Profile
    {
        public CarProfiles()
        {
            CreateMap<Car, ViewCarDTO>()
                .ForMember(dest => dest.Location, src => 
                    src.MapFrom(s => s.Location.City))
                .ForMember(dest => dest.Features, src => 
                    src.MapFrom(s => s.CarFeatures.Select(c => c.Feature) ));
            CreateMap<ViewCarDTO, ViewCarsViewModel>();
            CreateMap<Feature, FeatureDTO>();
            CreateMap<FeatureDTO, CarFeatureViewModel>();
            CreateMap<Car, RentACarDTO>()
                .ForMember(dest => dest.City, src =>
                    src.MapFrom(s => s.Location.City));
            CreateMap<Car, CarInformationDTO>()
                .ForMember(dest => dest.City, src =>
                    src.MapFrom(s => s.Location.City))
                .ForMember(dest => dest.Id, src => 
                    src.MapFrom(s => s.Id.ToString()));
            CreateMap<CarInformationDTO, CarInformationViewModel>();
            CreateMap<AddCarDTO, AddCarViewModel>();
        }
    }
}
