using Xeptions;

namespace FMFT.Web.Server.Models.Users.Exceptions
{
    public class RegisterUserWithLoginValidationException : Xeption
    {
        public RegisterUserWithLoginValidationException() 
            : base("Invalid RegisterUserWithLogin model. Please correct the errors and try again.")
        {

        }

        public RegisterUserWithLoginValidationException(Exception innerException, System.Collections.IDictionary data)
            : base(message: "Invalid RegisterUserWithLogin model. Please correct the errors and try again.",
                  innerException,
                  data)
        { }
    }
}
