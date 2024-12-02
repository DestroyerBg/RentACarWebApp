using AutoMapper;
using RentACar.Data.Models;
using RentACar.DTO.Category;
using RentACar.Web.ViewModels.Category;

namespace RentACar.Core.Infrastructure.AutoMapperProfiles
{
    public class CategoryProfiles : Profile
    {
        public CategoryProfiles()
        {
            CreateMap<Category, CategoryDTO>()
                .ForMember(dest => dest.Id, src =>
                    src.MapFrom(s => s.Id.ToString()));
            CreateMap<CategoryDTO, CategoryViewModel>();
            CreateMap<CategoryViewModel, CategoryDTO>();
        }
    }
}
