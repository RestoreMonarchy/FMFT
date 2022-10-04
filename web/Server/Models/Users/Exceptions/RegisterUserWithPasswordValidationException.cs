using FMFT.Extensions.Exceptions.Attributes;
using Xeptions;

namespace FMFT.Web.Server.Models.Users.Exceptions
{
    [WrapWithServiceValidationException]
    public class RegisterUserWithPasswordValidationException : Xeption
    {
        public RegisterUserWithPasswordValidationException()
            : base("ERR005: Register with password validation problem")
        { }
    }
}
