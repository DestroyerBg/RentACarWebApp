using AutoMapper;
using RentACar.Data.Models;
using RentACar.DTO.Admin;
using RentACar.DTO.Car;
using RentACar.DTO.Reservation;
using RentACar.Web.ViewModels.Admin;
using RentACar.Web.ViewModels.Car;
using RentACar.Web.ViewModels.Reservation;
using static RentACar.Common.Constants.DatabaseModelsConstants.Common;
namespace RentACar.Core.Infrastructure.AutoMapperProfiles
{
    public class ReservationProfiles : Profile
    {
        public ReservationProfiles()
        {
            CreateMap<RentACarDTO, RentACarViewModel>();
            CreateMap<RentACarViewModel, CreateReservationDTO>()
                .ForMember(dest => dest.CarId, src =>
                    src.MapFrom(s => s.Id))
                .ForMember(dest => dest.InsuranceBenefits,
                    src => src.MapFrom(s => s.Benefits));
            CreateMap<CreateReservationDTO, ConfirmReservationDTO>();
            CreateMap<ConfirmReservationDTO, ConfirmReservationViewModel>();
            CreateMap<ConfirmReservationDTO, Reservation>()
                .ForMember(dest => dest.CarId, 
                    src => src.MapFrom(s => Guid.Parse(s.CarId)))
                .ForMember(dest => dest.LocationId,
                    src => src.MapFrom(s => Guid.Parse(s.LocationId)))
                .ForMember(dest => dest.CustomerId,
                    src => src.MapFrom(s => Guid.Parse(s.CustomerId)))
                .ForMember(dest => dest.InsuranceBenefits, src => 
                    src.MapFrom(s => s.InsuranceBenefits.Select(i => new ReservationInsuranceBenefit()
                    {
                        InsuranceBenefitId = i.Id,
                    })));
            CreateMap<ConfirmReservationDTO, ReservationDoneViewModel>();
            CreateMap<Reservation, ReservationDTO>()
                .ForMember(dest => dest.CarBrand, src =>
                    src.MapFrom(s => s.Car.Brand))
                .ForMember(dest => dest.CarModel, src =>
                    src.MapFrom(s => s.Car.Model))
                .ForMember(dest => dest.StartDate, src =>
                    src.MapFrom(s => s.StartDate.ToString(DateFormat)))
                .ForMember(dest => dest.EndDate, src =>
                    src.MapFrom(s => s.EndDate.ToString(DateFormat)))
                .ForMember(dest => dest.OrderNumber, src =>
                    src.MapFrom(s => s.OrderNumber.ToString()))
                .ForMember(dest => dest.TotalPrice, src =>
                    src.MapFrom(s => s.TotalPrice.ToString()))
                .ForMember(dest => dest.Id, src => 
                    src.MapFrom(s => s.Id.ToString()));
            CreateMap<ReservationDTO, ReservationViewModel>();
            CreateMap<Reservation, ReservationDetailsDTO>()
                .ForMember(dest => dest.CarBrand, src => 
                    src.MapFrom(s => s.Car.Brand))
                .ForMember(dest => dest.CarModel, src =>
                    src.MapFrom(s => s.Car.Model))
                .ForMember(dest => dest.CarImageUrl, 
                    src => src.MapFrom(s => s.Car.ImageUrl));
            CreateMap<ReservationDetailsDTO, ReservationDetailsViewModel>();
            CreateMap<Reservation, ManageReservationDTO>()
                .ForMember(dest => dest.Id, src => src.MapFrom(s => s.Id.ToString()))
                .ForMember(dest => dest.CarBrandAndModel, src =>
                    src.MapFrom(s => $"{s.Car.Brand} {s.Car.Model}"))
                .ForMember(dest => dest.AccountUsername, 
                    src => src.MapFrom(s => s.Customer.UserName));
            CreateMap<ManageReservationDTO, ManageReservationViewModel>();
        }
    }
}
