using Microsoft.AspNetCore.Mvc;
using RentACar.Core.Interfaces;
using RentACar.Data.Models;
using RentACar.Web.ViewModels.Account;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace RentACar.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService<ApplicationUser, Guid> userService;
        private readonly ILogger<AccountController> logger;
        public AccountController(IUserService<ApplicationUser, Guid> _userService, 
            ILogger<AccountController> _logger)
        {
            userService = _userService;
            logger = _logger;
        }
        [HttpGet]
        public IActionResult Login()
        {
            LoginViewModel model = userService.CreateBlankLoginViewModel();
            return View(model);
        }

        [HttpPost]

        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            SignInResult result = await userService.LoginUserAsync(model);
                

            if (result.Succeeded)
            {
                logger.LogInformation("User logged in.");
                return RedirectToAction("Index", "Home");
            }
            if (result.IsLockedOut)
            {
                logger.LogWarning("User account locked out.");
                return RedirectToAction("Lockout");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                return View(model);
            }
        }

        [HttpGet]
        public IActionResult Register()
        {
            RegisterViewModel model = userService.CreateBlankRegisterViewModel();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            bool result = await userService.RegisterUserAsync(model);

            if (result == true)
            {
                logger.LogInformation("User created a new account with password.");
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await userService.LogoutUserAsync();
            logger.LogInformation("User logged out.");
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Lockout()
        {
            return View();
        }

        
    }
}
