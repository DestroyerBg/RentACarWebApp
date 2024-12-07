using AutoMapper;
using RentACar.DTO.CustomerFeedback;
using RentACar.Web.ViewModels.CustomerFeedback;

namespace RentACar.Core.Infrastructure.AutoMapperProfiles
{
    public class CustomerFeedbackProfiles : Profile
    {
        public CustomerFeedbackProfiles()
        {
            CreateMap<SendFeedbackDTO, SendFeedbackViewModel>();
        }
    }
}
