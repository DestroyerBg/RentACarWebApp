using Microsoft.AspNetCore.Mvc;
using RentACar.Common.Messages;
using RentACar.Core.Interfaces;
using RentACar.Data.Models;
using RentACar.Web.ViewModels.Identity;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;
using static RentACar.Common.Messages.IdentityMessages;
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
                logger.LogInformation(Result.UserCreatedAccount);
            }

            return RedirectToAction("Index", "Home");
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
