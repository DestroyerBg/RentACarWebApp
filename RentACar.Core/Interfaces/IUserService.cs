using Microsoft.AspNetCore.Identity;
using RentACar.DTO.Identity;
using RentACar.Web.ViewModels.Identity;

namespace RentACar.Core.Interfaces
{
    public interface IUserService<TUser, TKey>
        where TUser : IdentityUser<TKey>
        where TKey : IEquatable<TKey>
    {
        Task<bool> RegisterUserAsync(RegisterViewModel model);
        Task<SignInResult> LoginUserAsync(LoginDTO model);
        Task<bool> LogoutUserAsync();
        RegisterViewModel CreateBlankRegisterViewModel();
        LoginViewModel CreateBlankLoginViewModel();
    }
}
