using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RentACar.Core.Interfaces;
using RentACar.Data.Models;
using RentACar.Data.Repository.Interfaces;
using RentACar.DTO.Car;
using RentACar.DTO.Reservation;

namespace RentACar.Core.Services
{
    public class ReservationService : BaseService, IReservationService
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

            reservation.OrderNumber = reservationRepository.GetAllAttached().Count();
            await reservationRepository.AddAsync(reservation);

            bool isCompleted = await reservationRepository.SaveChangesAsync();

            return isCompleted;
        }

        public async Task<IEnumerable<ReservationDTO>> GetAllReservationForUser(ApplicationUser user)
        {
            IEnumerable<ReservationDTO> reservations = await reservationRepository
                .GetAllAttached()
                .Include(c => c.Car)
                .Include(r => r.Customer)
                .Where(r => r.CustomerId == user.Id)
                .Select(r => mapperService.Map<ReservationDTO>(r))
                .ToListAsync();

            return reservations;
        }

        public async Task<ReservationDetailsDTO> GetReservationDetails(Guid id)
        {
            Reservation? reservation = await reservationRepository
                .GetAllAttached()
                .Include(c => c.Car)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (reservation == null)
            {
                return null;
            }

            ReservationDetailsDTO dto = mapperService.Map<ReservationDetailsDTO>(reservation);

            return dto;
        }
    }
}
