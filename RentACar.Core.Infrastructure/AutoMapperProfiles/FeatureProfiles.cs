using AutoMapper;
using RentACar.Data.Models;
using RentACar.DTO.Feature;
using RentACar.Web.ViewModels.Feature;

namespace RentACar.Core.Infrastructure.AutoMapperProfiles
{
    public class FeatureProfiles : Profile
    {
        public FeatureProfiles()
        {
            CreateMap<Feature, FeatureCheckboxDTO>()
                .ForMember(dest => dest.Id, src =>
                    src.MapFrom(s => s.Id.ToString()));
            CreateMap<FeatureCheckboxDTO, FeatureCheckboxViewModel>();
        }
    }
}
