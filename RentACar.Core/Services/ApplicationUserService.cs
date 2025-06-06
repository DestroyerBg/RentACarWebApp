﻿using System.Globalization;
using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using RentACar.Core.Interfaces;
using RentACar.Data;
using RentACar.Data.Models;
using RentACar.DTO.Identity;
using RentACar.DTO.User;
using static RentACar.Common.Constants.DatabaseModelsConstants.Common;
using static RentACar.Common.Messages.DatabaseModelsMessages.ApplicationUser;
namespace RentACar.Core.Services
{
    public class ApplicationUserService : BaseService, IUserService
    {
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole<Guid>> roleManager;
        private readonly RentACarDbContext dbContext;
        private readonly IUserStore<ApplicationUser> userStore;
        private readonly IEmailSender emailSender;
        private readonly IMapper mapperService;
        public ApplicationUserService(
            SignInManager<ApplicationUser> _signInManager,
            UserManager<ApplicationUser> _userManager,
            IUserStore<ApplicationUser> _userStore,
            IEmailSender _emailSender,
            IMapper _mapperService,
            RoleManager<IdentityRole<Guid>> _roleManager,
            RentACarDbContext _dbContext)
        {
            signInManager = _signInManager;
            userManager = _userManager;
            userStore = _userStore;
            emailSender = _emailSender;
            mapperService = _mapperService;
            roleManager = _roleManager;
            dbContext = _dbContext;
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
                await userManager.AddToRoleAsync(user, "User");
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

        public async Task<ManageUsersDTO> GetAllUsersWithAllRoles()
        {
            ICollection<UsersDTO> users = await userManager.Users
                .Select(u => mapperService.Map<UsersDTO>(u))
                .ToListAsync();

            ICollection<RoleDTO> roles = await roleManager.Roles
                .Select(r => mapperService.Map<RoleDTO>(r))
                .ToListAsync();

            foreach (UsersDTO usersDto in users)
            {
                usersDto.UserRoles = dbContext.UserRoles
                    .Where(r => r.UserId == Guid.Parse(usersDto.Id))
                    .AsEnumerable()
                    .Select(ur => new RoleDTO()
                    {
                        Id = ur.RoleId.ToString(),
                        Name = roles.FirstOrDefault(r => r.Id == ur.RoleId.ToString()).Name
                    })
                    .ToList();
            }

            ManageUsersDTO dto = new ManageUsersDTO()
            {
                Roles = roles,
                Users = users
            };

            return dto;
        }
    }
}
