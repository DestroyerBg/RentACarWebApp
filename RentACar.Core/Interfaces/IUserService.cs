using Microsoft.AspNetCore.Identity;

namespace RentACar.Core.Interfaces
{
    public interface IUserService<TUser> where TUser : IdentityUser
    {
        Task<TUser> CreateNewUserInstance();
        Task<bool> RegisterUserAsync<T>(T model);
        Task<bool> LoginUserAsync<T>(T model);
        Task<bool> LogoutUserAsync<T>();
    }
}
