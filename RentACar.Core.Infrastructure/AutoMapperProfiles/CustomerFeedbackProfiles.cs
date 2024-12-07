using AutoMapper;
using RentACar.Data.Models;
using RentACar.DTO.CustomerFeedback;
using RentACar.Web.ViewModels.CustomerFeedback;

namespace RentACar.Core.Infrastructure.AutoMapperProfiles
{
    public class CustomerFeedbackProfiles : Profile
    {
        public CustomerFeedbackProfiles()
        {
            CreateMap<SendFeedbackDTO, SendFeedbackViewModel>();
            CreateMap<SendFeedbackViewModel, SendFeedbackDTO>()
                .ForMember(dest => dest.Rating, src => src.MapFrom(s => s.Stars))
                .ForMember(dest => dest.Cars, src => src.Ignore());
            CreateMap<SendFeedbackDTO, CustomerFeedback>();
        }
    }
}
