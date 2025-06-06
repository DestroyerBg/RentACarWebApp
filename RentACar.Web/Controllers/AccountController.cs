﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RentACar.Core.Interfaces;
using RentACar.Data.Models;
using RentACar.DTO.Identity;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;
using static RentACar.Common.Messages.IdentityMessages;
using Microsoft.AspNetCore.Identity;
using RentACar.Common.Messages;
using RentACar.Web.ViewModels.Account;
using static RentACar.Common.Constants.DatabaseModelsConstants.Common;
using static RentACar.Common.Messages.DatabaseModelsMessages.ApplicationUser;
namespace RentACar.Web.Controllers
{
    public class AccountController : BaseController
    {
        private readonly IUserService userService;
        private readonly ILogger<AccountController> logger;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IMapper mapService;
        public AccountController(IUserService _userService, 
            ILogger<AccountController> _logger,
            UserManager<ApplicationUser> _userManager,
            IMapper _mapService)
        {
            userService = _userService;
            logger = _logger;
            mapService = _mapService;
            userManager = _userManager;
        }   
        [HttpGet]
        public IActionResult Login()
        {
            LoginViewModel model = mapService.Map<LoginDTO, LoginViewModel>(userService.CreateBlankLoginViewModel());
            
            return View(model);
        }

        [HttpPost]

        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            LoginDTO dto = mapService.Map<LoginViewModel, LoginDTO>(model);
            SignInResult result = await userService.LoginUserAsync(dto);
            
            if (result.Succeeded)
            {
                logger.LogInformation(Result.UserLoggedIn);
                return RedirectToAction("Index", "Home");
            }
            if (result.IsLockedOut)
            {
                logger.LogWarning(Warning.UserLocked);
                return RedirectToAction("Lockout");
            }
            else
            {
                ModelState.AddModelError(string.Empty, Error.InvalidLoginAttempt);
                return View(model);
            }
        }

        [HttpGet]
        public IActionResult Register()
        {
            RegisterViewModel model = mapService.Map<RegisterDTO, RegisterViewModel>(userService.CreateBlankRegisterViewModel());
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            RegisterDTO dto = mapService.Map<RegisterViewModel, RegisterDTO>(model);
            IdentityResult result = await userService.RegisterUserAsync(dto);

            if (result.Succeeded)
            {
                logger.LogInformation(Result.UserCreatedAccount);
                TempData[SuccessfullMessageString] = RegistrationSuccess;
                return RedirectToAction("Index", "Home");
            }
            else
            {
                foreach (IdentityError error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }

                return View(model);
            }
            
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await userService.LogoutUserAsync();
            logger.LogInformation(Result.UserLogout);
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Lockout()
        {
            return View();
        }

        [HttpGet]
        public IActionResult MyProfile()
        {
            return View();
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> EditProfile()
        {
            ApplicationUser? user = await userManager.GetUserAsync(User);

            EditProfileDTO dto =  userService.CreateEditProfileDTO(user);

            EditProfileViewModel model = mapService.Map<EditProfileViewModel>(dto);

            return View(model);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> EditProfile(EditProfileViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            ApplicationUser? user = await userManager.GetUserAsync(User);

            if (user.Id.ToString() != model.Id)
            {
                return View(model);
            }

            EditProfileDTO dto = mapService.Map<EditProfileDTO>(model);

            bool result = await userService.EditProfile(dto);

            if (result)
            {
                TempData[SuccessfullMessageString] = SuccessfullUpdatedProfile;
                return RedirectToAction("MyProfile");
            }

            return RedirectToAction("MyProfile");

        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> ChangePassword()
        {
            ChangePasswordDTO dto = userService.GenerateNewChangePasswordDto();
            return View(new ChangePasswordViewModel());
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            ChangePasswordDTO dto = mapService.Map<ChangePasswordDTO>(model);

            string result = await userService.ChangePasswordWithOldPassword(dto, User);

            if (result != ChangePasswordSuccess)
            {
                ModelState.AddModelError(string.Empty, result);
                return View(model);
            }

            TempData[SuccessfullMessageString] = result;
            return RedirectToAction("MyProfile");
            
        }

    }
}
