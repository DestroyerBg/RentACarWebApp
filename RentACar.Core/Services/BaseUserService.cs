using System.Globalization;
using System.Runtime.InteropServices.JavaScript;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using RentACar.Core.Interfaces;
using RentACar.Data.Models;
using RentACar.DTO.Identity;
using RentACar.Web.ViewModels.Identity;
using static RentACar.Common.Constants.DatabaseModelsConstants.ApplicationUser;
namespace RentACar.Core.Services
{
    public abstract class BaseUserService<TUser, TKey> : IUserService<TUser, TKey> 
        where TUser : ApplicationUser, new()
        where TKey : IEquatable<TKey>
    {
        private readonly SignInManager<TUser> signInManager;
        private readonly UserManager<TUser> userManager;
        private readonly IUserStore<TUser> userStore;
        private readonly IUserEmailStore<TUser> emailStore;
        private readonly IEmailSender emailSender;
        private readonly IMapper mapperService;

        protected BaseUserService(
            SignInManager<TUser> _signInManager,
            UserManager<TUser> _userManager,
            IUserStore<TUser> _userStore,
            IEmailSender _emailSender,
            IMapper _mapperService)
        {
            signInManager = _signInManager;
            userManager = _userManager;
            userStore = _userStore;
            emailSender = _emailSender;
            mapperService = _mapperService;
        }

        private TUser CreateNewUserInstance()
        {
            TUser user = Activator.CreateInstance<TUser>();

            return user;
        }

        public virtual RegisterDTO CreateBlankRegisterViewModel()
        {
            return new RegisterDTO();
        }

        public virtual async Task<IdentityResult> RegisterUserAsync(RegisterDTO dto)
        {
            TUser user = CreateNewUserInstance();
            user = mapperService.Map<RegisterDTO, TUser>(dto);

            await userStore.SetUserNameAsync(user, dto.Username, CancellationToken.None);
            await userManager.SetEmailAsync(user, dto.Email);
           
            IdentityResult result = await userManager.CreateAsync(user, dto.Password);

            return result;
        }

        public virtual LoginDTO CreateBlankLoginViewModel()
        {
            return new LoginDTO();
        }

        public virtual async Task<SignInResult> LoginUserAsync(LoginDTO dto)
        {
            TUser user = await userManager.FindByEmailAsync(dto.Email);
            if (user == null)
            {
                return new SignInResult();
            }

            SignInResult result =
                await signInManager
                    .PasswordSignInAsync(user.UserName,
                        dto.Password, dto.RememberMe, lockoutOnFailure: true);

            return result;
        }

        public virtual async Task<bool> LogoutUserAsync()
        {
            await signInManager.SignOutAsync();
            
            return true;
        }

        
    }
}
