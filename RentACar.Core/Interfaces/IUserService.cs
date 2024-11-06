using Microsoft.AspNetCore.Identity;
using RentACar.Data.Models;
using RentACar.DTO.Identity;
namespace RentACar.Core.Interfaces
{
    public interface IUserService<TUser, TKey>
        where TUser : ApplicationUser
        where TKey : IEquatable<TKey>
    {
        Task<IdentityResult> RegisterUserAsync(RegisterDTO model);
        Task<SignInResult> LoginUserAsync(LoginDTO model);
        Task<bool> LogoutUserAsync();
        RegisterDTO CreateBlankRegisterViewModel();
        LoginDTO CreateBlankLoginViewModel();
    }
}
