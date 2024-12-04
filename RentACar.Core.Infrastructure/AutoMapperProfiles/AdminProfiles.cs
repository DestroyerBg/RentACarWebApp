using AutoMapper;
using RentACar.Data.Models;
using RentACar.DTO.Admin;
using RentACar.DTO.User;
using RentACar.Web.ViewModels.Admin;

namespace RentACar.Core.Infrastructure.AutoMapperProfiles
{
    public class AdminProfiles : Profile
    {
        public AdminProfiles()
        {
            CreateMap<DashboardDTO, DashboardViewModel>();
        }
    }
}
