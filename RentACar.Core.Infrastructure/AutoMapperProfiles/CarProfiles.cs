using AutoMapper;
using RentACar.Data.Models;
using RentACar.DTO.Car;
using RentACar.Web.ViewModels.Admin;
using RentACar.Web.ViewModels.Car;
using static RentACar.Common.Constants.DatabaseModelsConstants.Common;

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
                    src.MapFrom(s => s.CarFeatures.Select(c => c.Feature)));
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
            CreateMap<AddCarDTO, AddCarViewModel>()
                .ForMember(dest => dest.CarImage, src =>
                    src.MapFrom(s => s.CarImage));
            CreateMap<AddCarViewModel, AddCarDTO>();
            CreateMap<AddCarDTO, Car>()
                .ForMember(src => src.CategoryId, dest =>
                    dest.MapFrom(d => Guid.Parse(d.CategoryId)))
                .ForMember(dest => dest.LocationId,
                    src => src.MapFrom(s => Guid.Parse(s.LocationId)))
                .ForMember(dest => dest.ImageUrl,
                    src => src.MapFrom(s => s.CarImageUrl));
            CreateMap<EditCarDTO, EditCarViewModel>();
            CreateMap<EditCarViewModel, EditCarDTO>()
                .ForMember(dest => dest.CarImage, src =>
                    src.MapFrom(s => s.CarImage)); ;
            CreateMap<EditCarDTO, Car>()
                .ForMember(dest => dest.Id, src =>
                    src.MapFrom(s => Guid.Parse(s.Id)))
                .ForMember(dest => dest.CarFeatures, src => src.Ignore())
                .ForMember(dest => dest.LocationId, src =>
                    src.MapFrom(s => Guid.Parse(s.Locations.FirstOrDefault(l => l.Selected).Value)))
                    .ForMember(dest => dest.CategoryId, src =>
                    src.MapFrom(s => Guid.Parse(s.Categories.FirstOrDefault(c => c.Selected).Value)))
                .ForMember(dest => dest.YearOfManufacture, src =>
                    src.MapFrom(s => s.YearOfManufacture))
                .ForMember(dest => dest.ImageUrl, src =>
                    src.MapFrom(s => s.CarImageUrl));
            CreateMap<Car, FeedbackCarDTO>()
                .ForMember(dest => dest.BrandAndModel, src =>
                    src.MapFrom(s => $"{s.Brand} {s.Model}"))
                .ForMember(dest => dest.Id, src => src.MapFrom(s => s.Id.ToString()));
            CreateMap<FeedbackCarDTO, FeedbackCarViewModel>();
        }
    }
}
