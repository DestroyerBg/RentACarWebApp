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
        Task<bool> AddCar(AddCarDTO dto);
        Task<bool> FindCarByRegistrationNumberAsync(string registrationNumber);
        Task<bool> DeleteCarAsync(Guid id);
        Task<EditCarDTO> CreateEditCarDto(Guid id);
        Task<bool> EditCarAsync(EditCarDTO dto);

        Task<bool> FindCarByIdAsync(Guid id);
    }
}
