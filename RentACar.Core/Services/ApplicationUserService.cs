using System.Globalization;
using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using RentACar.Core.Interfaces;
using RentACar.Data.Models;
using RentACar.DTO.Identity;
using static RentACar.Common.Constants.DatabaseModelsConstants.Common;
using static RentACar.Common.Constants.DatabaseModelsConstants.ApplicationUser;
namespace RentACar.Core.Services
{
    public class ApplicationUserService : IUserService
    {
        private SignInManager<ApplicationUser> signInManager;
        private UserManager<ApplicationUser> userManager;
        private IUserStore<ApplicationUser> userStore;
        private IEmailSender emailSender;
        private IMapper mapperService;
        public ApplicationUserService(
            SignInManager<ApplicationUser> _signInManager,
            UserManager<ApplicationUser> _userManager,
            IUserStore<ApplicationUser> _userStore,
            IEmailSender _emailSender,
            IMapper _mapperService)
        {
            signInManager = _signInManager;
            userManager = _userManager;
            userStore = _userStore;
            emailSender = _emailSender;
            mapperService = _mapperService;
        }

        private ApplicationUser CreateNewUserInstance()
        {
            ApplicationUser user = Activator.CreateInstance<ApplicationUser>();

            return user;
        }

        public async Task<bool> LogoutUserAsync()
        {
            await signInManager.SignOutAsync();

            return true;
        }

        public virtual RegisterDTO CreateBlankRegisterViewModel()
        {
            return new RegisterDTO();
        }

        public virtual async Task<IdentityResult> RegisterUserAsync(RegisterDTO dto)
        {
            ApplicationUser? existingUserByEmail = await userManager.FindByEmailAsync(dto.Email);
            if (existingUserByEmail != null)
            {
                return IdentityResult.Failed(new IdentityError
                {
                    Code = "DuplicateEmail",
                    Description = UserWithThatEmailExists
                });
            }

            ApplicationUser? existingUserByUsername = await userManager.FindByNameAsync(dto.Username);
            if (existingUserByUsername != null)
            {
                return IdentityResult.Failed(new IdentityError
                {
                    Code = "DuplicateUsername",
                    Description = UserWithThatUsernameExists
                });
            }
            ApplicationUser user = CreateNewUserInstance();
            user = mapperService.Map<RegisterDTO, ApplicationUser>(dto);

            IdentityResult result;
            await userManager.SetUserNameAsync(user, dto.Username);
            
            await userManager.SetEmailAsync(user, dto.Email);

            result = await userManager.CreateAsync(user, dto.Password);

            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(user, "Customer");
            }

            return result;
        }

        public virtual LoginDTO CreateBlankLoginViewModel()
        {
            return new LoginDTO();
        }

        public virtual async Task<SignInResult> LoginUserAsync(LoginDTO dto)
        {
            ApplicationUser user = await userManager.FindByEmailAsync(dto.Email);
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


        public EditProfileDTO CreateEditProfileDTO(ApplicationUser user)
        {
            EditProfileDTO dto = mapperService.Map<EditProfileDTO>(user);

            return dto;
        }

        public async Task<bool> EditProfile(EditProfileDTO dto)
        {
            ApplicationUser? user = await userManager.FindByIdAsync(dto.Id);

            if (user == null)
            {
                return false;
            }

            if (user.Email != dto.Email)
            {
                await userManager.SetEmailAsync(user, dto.Email);
            }

            if (user.UserName != dto.Username)
            {
                await userManager.SetUserNameAsync(user, dto.Username);
            }

            user.FirstName = dto.FirstName;
            user.LastName = dto.LastName;
            user.BirthDate = DateTime.ParseExact(dto.BirthDate, DateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None);
            user.PhoneNumber = dto.PhoneNumber;

            IdentityResult result = await userManager.UpdateAsync(user);

            return result.Succeeded;
        }

        public string GenerateChangePasswordNumberAsync()
        {
            Random random = new Random();

            string rndNumber = random.Next(100000, 999999).ToString();

            return rndNumber;
        }

        public ChangePasswordDTO GenerateNewChangePasswordDto()
        {
            return new ChangePasswordDTO();
        }

        public async Task<string> ChangePasswordWithOldPassword(ChangePasswordDTO dto, ClaimsPrincipal principal)
        {
            ApplicationUser? user = await userManager.GetUserAsync(principal);

            if (user == null)
            {
                return CannotFindLoggedInUser;
            }

            if (dto.NewPassword != dto.ConfirmPassword)
            {
                return NewPasswordIsDifferentThanOldPassword;
            }

            string token = await userManager.GeneratePasswordResetTokenAsync(user);

            IdentityResult result = await userManager.ChangePasswordAsync(user, dto.OldPassword, dto.NewPassword);

            if (result.Errors.Any())
            {
                return ErrorWhenChangingPasswords;
            }

            return ChangePasswordSuccess;
        }
    }
}
