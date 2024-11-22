using AutoMapper;
using RentACar.Core.Interfaces;
using RentACar.Data.Models;
using RentACar.Data.Repository;
using RentACar.Data.Repository.Interfaces;
using RentACar.DTO.Car;

namespace RentACar.Core.Services
{
    public class ReservationService : IReservationService
    {
        private readonly IMapper mapperService;
        private readonly IRepository<Reservation, Guid> reservationRepository;
        public ReservationService(IMapper _mapperService,
            IRepository<Reservation, Guid> _reservationRepository)
        {
            mapperService = _mapperService;
            reservationRepository = _reservationRepository;
        }
        public async Task<bool> CreateReservation(ConfirmReservationDTO dto)
        {
            Reservation reservation = mapperService.Map<Reservation>(dto);

            await reservationRepository.AddAsync(reservation);

            bool isCompleted = await reservationRepository.SaveChangesAsync();

            return isCompleted;
        }
    }
}
