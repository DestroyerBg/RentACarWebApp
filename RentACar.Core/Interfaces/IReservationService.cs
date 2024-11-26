using RentACar.Data.Models;
using RentACar.DTO.Car;
using RentACar.DTO.Reservation;

namespace RentACar.Core.Interfaces
{
    public interface IReservationService
    {
        Task<bool> CreateReservation(ConfirmReservationDTO dto);
        Task<IEnumerable<ReservationDTO>> GetAllReservationForUser(ApplicationUser user);
        Task<ReservationDetailsDTO> GetReservationDetails(Guid id);
    }
}
