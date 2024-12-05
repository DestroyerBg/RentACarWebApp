using System.Security.Claims;
using RentACar.Data.Models;
using RentACar.DTO.Admin;
using RentACar.DTO.Car;
using RentACar.DTO.Role;
using RentACar.DTO.User;

namespace RentACar.Core.Interfaces
{
    public interface IAdminService
    { 
        Task<DashboardDTO> GetAppInfo();
        Task<IEnumerable<CarInformationDTO>> GetCarsInformation();
        Task<bool> IsUserAdmin(ClaimsPrincipal claim);

        Task<(bool success, string message)> SetRoleToUser(SetRoleDTO dto, ClaimsPrincipal claim);

        Task<bool> DeleteRoleFromUser(SetRoleDTO dto);

        Task<bool> DeleteUser(DeleteUserDTO dto);

        Task<bool> IsModifyingOwnRole(ClaimsPrincipal claim, string targetUserId);

        Task<bool> CheckIfCurrentAdminIsSuperAdmin(ClaimsPrincipal claim);
    }
}
