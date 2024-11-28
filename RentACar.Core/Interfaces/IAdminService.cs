using RentACar.DTO.Admin;
using RentACar.DTO.Car;

namespace RentACar.Core.Interfaces
{
    public interface IAdminService
    { 
        Task<DashboardDTO> GetAppInfo();
        Task<IEnumerable<CarInformationDTO>> GetCarsInformation();
    }
}
