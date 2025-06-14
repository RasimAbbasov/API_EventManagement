using Microsoft.AspNetCore.Identity;
using System.Resources;

namespace API_EventManagement
{
    //Handle Identity errors with custom messages
    public class CustomErrorDescriber: IdentityErrorDescriber
    {
        public override IdentityError PasswordRequiresDigit()
        {
            return new IdentityError
            {
                Code = nameof(PasswordRequiresDigit),
                Description = "Password must contain at least one digit."
            };
        }
    }
}
