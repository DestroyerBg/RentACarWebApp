using AutoMapper;
using Microsoft.AspNetCore.Identity;
using RentACar.Data.Models;
using RentACar.DTO.Role;
using RentACar.DTO.User;
using RentACar.Web.ViewModels.Role;
using RentACar.Web.ViewModels.User;

namespace RentACar.Core.Infrastructure.AutoMapperProfiles
{
    public class UserProfiles : Profile
    {
        public UserProfiles()
        {
            CreateMap<ApplicationUser, UsersDTO>()
                .ForMember(dest => dest.Email, src =>
                    src.MapFrom(s => s.Email))
                .ForMember(dest => dest.Username, src =>
                    src.MapFrom(s => s.UserName))
                .ForMember(dest => dest.Id, src => 
                    src.MapFrom(s => s.Id.ToString()));

            CreateMap<IdentityRole<Guid>, RoleDTO>()
                .ForMember(dest => dest.Id, src => 
                    src.MapFrom(s => s.Id.ToString()))
                .ForMember(dest => dest.Name, src => 
                    src.MapFrom(s => s.Name));

            CreateMap<RoleDTO, RoleViewModel>();
            CreateMap<UsersDTO, UserViewModel>();
            CreateMap<ManageUsersDTO, ManagerUsersViewModel>();
            CreateMap<SetRoleViewModel, SetRoleDTO>();
        }
    }
}
