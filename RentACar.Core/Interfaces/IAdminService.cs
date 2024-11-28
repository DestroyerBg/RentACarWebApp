using RentACar.DTO.Admin;

namespace RentACar.Core.Interfaces
{
    public interface IAdminService
    { 
        Task<DashboardDTO> GetAppInfo();
    }
}
