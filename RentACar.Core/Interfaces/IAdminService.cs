using RentACar.DTO.Admin;
using RentACar.DTO.Car;
using RentACar.DTO.User;

namespace RentACar.Core.Interfaces
{
    public interface IAdminService
    { 
        Task<DashboardDTO> GetAppInfo();
        Task<IEnumerable<CarInformationDTO>> GetCarsInformation();
    }
}
