using Microsoft.AspNetCore.Identity;
using RentACar.Data.Models;
using RentACar.DTO.Identity;
using System.Security.Claims;
namespace RentACar.Core.Interfaces
{
    public interface IUserService
    {
        Task<IdentityResult> RegisterUserAsync(RegisterDTO model);
        Task<SignInResult> LoginUserAsync(LoginDTO model);
        Task<bool> LogoutUserAsync();
        RegisterDTO CreateBlankRegisterViewModel();
        LoginDTO CreateBlankLoginViewModel();
        Task<ApplicationUser> GetUserByIdAsync(ClaimsPrincipal claim);
        EditProfileDTO CreateEditProfileDTO(ApplicationUser user);
        Task<bool> EditProfile(EditProfileDTO dto);
    }
}
