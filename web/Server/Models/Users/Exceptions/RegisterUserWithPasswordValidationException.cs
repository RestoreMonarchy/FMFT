using Xeptions;

namespace FMFT.Web.Server.Models.Users.Exceptions
{
    public class RegisterUserWithPasswordValidationException : Xeption
    {
        public RegisterUserWithPasswordValidationException()
            : base("ERR005: Register with password validation problem")
        { }
    }
}
