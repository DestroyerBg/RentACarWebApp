using AutoMapper;
using RentACar.DTO.Admin;
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
