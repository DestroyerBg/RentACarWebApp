using Microsoft.AspNetCore.Identity;
using static RentACar.Common.Constants.DatabaseModelsConstants.ApplicationUser;
namespace RentACar.Web.ErrorDescribers
{
    public class CustomIdentityErrorDescriber : IdentityErrorDescriber
    {
        public override IdentityError PasswordRequiresDigit()
        {
            return new IdentityError()
            {
                Code = nameof(PasswordRequiresDigit),
                Description = PasswordRequiresAtLeastOneDigit
            };
        }

        public override IdentityError PasswordRequiresLower()
        {
            return new IdentityError()
            {
                Code = nameof(PasswordRequiresDigit),
                Description = PasswordRequiresAtLeastOneDigit
            };
        }
    }
}
