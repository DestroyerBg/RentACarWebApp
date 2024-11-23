using AutoMapper;
using RentACar.Data.Models;
using RentACar.DTO.Identity;
using RentACar.Web.ViewModels.Identity;
using static RentACar.Common.Constants.DatabaseModelsConstants.Common;
namespace RentACar.Core.Infrastructure.AutoMapperProfiles
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
            CreateMap<ApplicationUser, EditProfileDTO>()
                .ForMember(dest => dest.Email, src =>
                    src.MapFrom(s => s.Email))
                .ForMember(dest => dest.Username, src => 
                    src.MapFrom(s => s.UserName))
                .ForMember(dest => dest.BirthDate, src => 
                    src.MapFrom(s => s.BirthDate.ToString(DateFormat)))
                .ForMember(dest => dest.PhoneNumber, src => 
                    src.MapFrom(s => s.PhoneNumber));
            CreateMap<EditProfileDTO, EditProfileViewModel>();
        }
    }
}
