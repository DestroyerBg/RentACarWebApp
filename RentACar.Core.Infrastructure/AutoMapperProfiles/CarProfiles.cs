﻿using AutoMapper;
using RentACar.Data.Models;
using RentACar.DTO.Car;
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
        }
    }
}