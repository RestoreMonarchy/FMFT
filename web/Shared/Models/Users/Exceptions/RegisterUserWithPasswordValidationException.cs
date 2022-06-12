using Xeptions;

namespace FMFT.Web.Shared.Models.Users.Exceptions
{
    public class RegisterUserWithPasswordValidationException : Xeption
    {
        public RegisterUserWithPasswordValidationException()
            : base("Invalid RegisterUserWithPassword model. Please correct the errors and try again.")
        { }

        public RegisterUserWithPasswordValidationException(Exception innerException, System.Collections.IDictionary data)
            : base(message: "Invalid RegisterUserWithPassword model. Please correct the errors and try again.",
                  innerException,
                  data)
        { }
    }
}
