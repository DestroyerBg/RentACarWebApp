using AutoMapper;
using RentACar.Data.Models;
using RentACar.DTO.Car;
using RentACar.DTO.Reservation;
using RentACar.Web.ViewModels.Car;
using RentACar.Web.ViewModels.Reservation;

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
        }
    }
}
