using AutoMapper;
using RentACar.Data.Models;
using RentACar.DTO.InsuranceBenefit;
using RentACar.Web.ViewModels.InsuranceBenefit;

namespace RentACar.Core.Infrastructure.AutoMapperProfiles
{
    public class InsuranceBenefitProfiles : Profile
    {
        public InsuranceBenefitProfiles()
        {
            CreateMap<InsuranceBenefit, InsuranceBenefitDTO>();
            CreateMap<InsuranceBenefitDTO, InsuranceBenefitViewModel>();
            CreateMap<InsuranceBenefitViewModel, InsuranceBenefitDTO>();
            CreateMap<InsuranceBenefitDTO, InsuranceBenefit>();
        }
    }
}
