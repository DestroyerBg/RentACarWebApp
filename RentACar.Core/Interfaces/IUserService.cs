using Microsoft.AspNetCore.Identity;
using RentACar.Web.ViewModels.Account;

namespace RentACar.Core.Interfaces
{
    public interface IUserService<TUser, TKey>
        where TUser : IdentityUser<TKey>
        where TKey : IEquatable<TKey>
    {
        Task<bool> RegisterUserAsync(RegisterViewModel model);
        Task<SignInResult> LoginUserAsync(LoginViewModel model);
        Task<bool> LogoutUserAsync();
        RegisterViewModel CreateBlankRegisterViewModel();
        LoginViewModel CreateBlankLoginViewModel();
    }
}
