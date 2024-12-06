using RentACar.DTO.Car;
using RentACar.DTO.Reservation;
using RentACar.DTO.Result;

namespace RentACar.Core.Interfaces
{
    public interface ICarService
    { 
        Task<IEnumerable<ViewCarDTO>> GetCarsAsync();
        Task<RentACarDTO> ReserveACar(Guid carId);
        Task<ConfirmReservationDTO> CreateReservationConfirmation(CreateReservationDTO reservationDTO);
        Task<AddCarDTO> CreateAddCarDto();
        Task<ResultWithErrors> AddCar(AddCarDTO dto);
        Task<bool> DeleteCarAsync(Guid id);
        Task<EditCarDTO> CreateEditCarDto(Guid id);
        Task<ResultWithErrors> EditCarAsync(EditCarDTO dto);

    }
}
