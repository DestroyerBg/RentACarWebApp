using RentACar.DTO.Car;

namespace RentACar.Core.Interfaces
{
    public interface IReservationService
    {
        Task<bool> CreateReservation(ConfirmReservationDTO dto);
    }
}
