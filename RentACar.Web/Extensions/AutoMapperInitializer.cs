using RentACar.DTO.Identity;
using RentACar.Services.Infrastructure.AutoMapper;
using RentACar.Web.ViewModels.Identity;

namespace RentACar.Web.Extensions
{
    public static class AutoMapperInitializer
    {
        public static void RegisterAutoMapper()
        {
            AutoMapperConfig.RegisterMappings(typeof(LoginViewModel).Assembly);
            AutoMapperConfig.RegisterMappings(typeof(LoginDTO).Assembly);
        }
    }
}
