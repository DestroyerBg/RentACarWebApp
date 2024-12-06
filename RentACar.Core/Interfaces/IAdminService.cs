using System.Security.Claims;
using RentACar.Data.Models;
using RentACar.DTO.Admin;
using RentACar.DTO.Car;
using RentACar.DTO.Result;
using RentACar.DTO.Role;
using RentACar.DTO.User;

namespace RentACar.Core.Interfaces
{
    public interface IAdminService
    { 
        Task<DashboardDTO> GetAppInfo();
        Task<IEnumerable<CarInformationDTO>> GetCarsInformation();
        Task<bool> IsUserAdmin(ClaimsPrincipal claim);

        Task<Result> SetRoleToUser(SetRoleDTO dto, ClaimsPrincipal claim);

        Task<Result> DeleteRoleFromUser(SetRoleDTO dto, ClaimsPrincipal claim);

        Task<Result> DeleteUser(DeleteUserDTO dto, ClaimsPrincipal claim);

        Task<bool> IsModifyingOwnRole(ClaimsPrincipal claim, string targetUserId);

        Task<bool> CheckIfCurrentAdminIsSuperAdmin(ClaimsPrincipal claim);
    }
}
