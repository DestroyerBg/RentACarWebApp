using AutoMapper;
using RentACar.Data.Models;
using RentACar.DTO.Identity;
using RentACar.Web.ViewModels.Identity;

namespace RentACar.Services.Infrastructure.AutoMapperProfiles
{
    public class IdentityProfiles : Profile
    {
        public IdentityProfiles()
        {
            CreateMap<RegisterViewModel, RegisterDTO>();
            CreateMap<RegisterDTO, RegisterViewModel>();
            CreateMap<RegisterDTO, ApplicationUser>();
            CreateMap<LoginViewModel, LoginDTO>();
            CreateMap<LoginDTO, LoginViewModel>();
        }
    }
}
