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
            CreateMap<CustomerFeedback, UserFeedbackDTO>()
                .ForMember(dest => dest.CreatedOn, src => src.MapFrom(s => s.DateOfSubmission))
                .ForMember(dest => dest.CarBrandAndModel, src => src.MapFrom(s => $"{s.Car.Brand} {s.Car.Model}"))
                .ForMember(dest => dest.Stars, src => src.MapFrom(s => s.Rating))
                .ForMember(dest => dest.CustomerUsername, src => 
                    src.MapFrom(s => s.Customer.UserName));
            CreateMap<UserFeedbackDTO, UserFeedbackViewModel>();
        }
    }
}
