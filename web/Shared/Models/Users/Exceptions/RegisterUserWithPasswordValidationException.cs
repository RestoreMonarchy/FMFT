using Xeptions;

namespace FMFT.Web.Shared.Models.Users.Exceptions
{
    public class RegisterUserWithPasswordValidationException : Xeption
    {
        public RegisterUserWithPasswordValidationException()
            : base("Invalid RegisterUserWithPassword model. Please correct the errors and try again.")
        {

        }
    }
}
