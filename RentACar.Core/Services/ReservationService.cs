using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RentACar.Core.Interfaces;
using RentACar.Data.Models;
using RentACar.Data.Repository;
using RentACar.Data.Repository.Interfaces;
using RentACar.DTO.Car;
using RentACar.DTO.Reservation;

namespace RentACar.Core.Services
{
    public class ReservationService : BaseService, IReservationService
    {
        private readonly IMapper mapperService;
        private readonly IRepository<Reservation, Guid> reservationRepository;
        private readonly IRepository<Car, Guid> carRepository;
        public ReservationService(IMapper _mapperService,
            IRepository<Reservation, Guid> _reservationRepository,
            IRepository<Car, Guid> _carRepository)
        {
            mapperService = _mapperService;
            reservationRepository = _reservationRepository;
            carRepository = _carRepository;
        }
        public async Task<bool> CreateReservation(ConfirmReservationDTO dto)
        {
            dto.InsuranceBenefits = dto.InsuranceBenefits.Where(i => i.IsChecked == true).ToList();
            Reservation reservation = mapperService.Map<Reservation>(dto);

            reservation.OrderNumber = reservationRepository.GetAllAttached().Count() + 1;
            await reservationRepository.AddAsync(reservation);

            Car? car = await carRepository.GetByIdAsync(Guid.Parse(dto.CarId));

            car.Reservations.Add(reservation);

            bool isCompleted = await reservationRepository.SaveChangesAsync();

            if (isCompleted == true)
            {
                car.IsHired = true;
            }
            
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
                .Include(r => r.InsuranceBenefits)
                .ThenInclude(r => r.InsuranceBenefit)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (reservation == null)
            {
                return null;
            }

            reservation.InsuranceBenefits = reservation.InsuranceBenefits.Where(i => i.ReservationId == reservation.Id).ToList();

            ReservationDetailsDTO dto = mapperService.Map<ReservationDetailsDTO>(reservation);

            return dto;
        }
    }
}
