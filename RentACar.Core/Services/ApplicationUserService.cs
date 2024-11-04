using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using RentACar.Data.Models;

namespace RentACar.Core.Services
{
    public class ApplicationUserService : BaseUserService<ApplicationUser, Guid>
    {
        public ApplicationUserService(
            SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager,
            IUserStore<ApplicationUser> userStore,
            IEmailSender emailSender) : base(signInManager, userManager, userStore,emailSender)
        {
            
        }

    }
}
