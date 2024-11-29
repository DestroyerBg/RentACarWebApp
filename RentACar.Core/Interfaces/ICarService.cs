using RentACar.DTO.Car;
using RentACar.DTO.Reservation;

namespace RentACar.Core.Interfaces
{
    public interface ICarService
    { 
        Task<IEnumerable<ViewCarDTO>> GetCarsAsync();
        Task<RentACarDTO> ReserveACar(Guid carId);
        Task<ConfirmReservationDTO> CreateReservationConfirmation(CreateReservationDTO reservationDTO);

        Task<AddCarDTO> CreateAddCarDto();
    }
}
