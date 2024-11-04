using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using RentACar.Core.Interfaces;
using RentACar.Web.ViewModels.Identity;

namespace RentACar.Core.Services
{
    public abstract class BaseUserService<TUser, TKey> : IUserService<TUser, TKey> 
        where TUser : IdentityUser<TKey>
        where TKey : IEquatable<TKey>
    {
        private readonly SignInManager<TUser> signInManager;
        private readonly UserManager<TUser> userManager;
        private readonly IUserStore<TUser> userStore;
        private readonly IUserEmailStore<TUser> emailStore;
        private readonly IEmailSender emailSender;

        protected BaseUserService(
            SignInManager<TUser> _signInManager,
            UserManager<TUser> _userManager,
            IUserStore<TUser> _userStore,
            IEmailSender _emailSender)
        {
            signInManager = _signInManager;
            userManager = _userManager;
            userStore = _userStore;
            emailSender = _emailSender;
        }

        private TUser CreateNewUserInstance()
        {
            TUser user = Activator.CreateInstance<TUser>();

            return user;
        }

        public virtual RegisterViewModel CreateBlankRegisterViewModel()
        {
            return new RegisterViewModel();
        }

        public virtual async Task<bool> RegisterUserAsync(RegisterViewModel model)
        {
            TUser user = CreateNewUserInstance();

            await userStore.SetUserNameAsync(user, model.Email, CancellationToken.None);
            await userManager.SetEmailAsync(user, model.Email);
            IdentityResult result = await userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                return true;
            }

            return false;
        }

        public virtual LoginViewModel CreateBlankLoginViewModel()
        {
            return new LoginViewModel();
        }

        public virtual async Task<SignInResult> LoginUserAsync(LoginViewModel model)
        {
            SignInResult result =
                await signInManager
                    .PasswordSignInAsync(model.Email, 
                        model.Password, model.RememberMe, lockoutOnFailure: true);

            return result;
        }

        public virtual async Task<bool> LogoutUserAsync()
        {
            await signInManager.SignOutAsync();
            
            return true;
        }

        
    }
}
