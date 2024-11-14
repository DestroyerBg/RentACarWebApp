﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RentACar.Core.Interfaces;
using RentACar.Data.Models;
using RentACar.DTO.Identity;
using RentACar.Web.ViewModels.Identity;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;
using static RentACar.Common.Messages.IdentityMessages;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace RentACar.Web.Controllers
{
    public class AccountController : BaseController
    {
        private readonly IUserService<ApplicationUser, Guid> userService;
        private readonly ILogger<AccountController> logger;
        private readonly IMapper mapService;
        public AccountController(IUserService<ApplicationUser, Guid> _userService, 
            ILogger<AccountController> _logger,
            IMapper _mapService)
        {
            userService = _userService;
            logger = _logger;
            mapService = _mapService;
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

        
    }
}
