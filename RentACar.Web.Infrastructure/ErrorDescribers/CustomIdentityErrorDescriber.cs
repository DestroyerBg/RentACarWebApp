using Microsoft.AspNetCore.Identity;
using static RentACar.Common.Constants.DatabaseModelsConstants.ApplicationUser;
namespace RentACar.Web.Infrastructure.ErrorDescribers
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
                Code = nameof(PasswordRequiresLower),
                Description = PasswordRequireAtLeastOneLowerCase
            };
        }

        public override IdentityError PasswordRequiresUpper()
        {
            return new IdentityError()
            {
                Code = nameof(PasswordRequiresUpper),
                Description = PasswordRequireAtLeastOneUpperCase,
            };
        }

        public override IdentityError PasswordRequiresNonAlphanumeric()
        {
            return new IdentityError()
            {
                Code = nameof(PasswordRequiresNonAlphanumeric),
                Description = PasswordRequireAtLeastOneNonAlphaNumericCharacter,
            };
        }

        public override IdentityError PasswordTooShort(int length)
        {
            return new IdentityError()
            {
                Code = nameof(PasswordTooShort),
                Description = String.Format(PasswordTooShortMessage, PasswordMinlength),
            };
        }

    }
}
