using AutoMapper;
using RentACar.Data.Models;
using RentACar.DTO.Location;
using RentACar.Web.ViewModels.Location;

namespace RentACar.Core.Infrastructure.AutoMapperProfiles
{
    public class LocationProfiles : Profile
    {
        public LocationProfiles()
        {
            CreateMap<Location, LocationDTO>();
            CreateMap<LocationDTO, LocationViewModel>();
            CreateMap<LocationViewModel, LocationDTO>();
        }
    }
}
