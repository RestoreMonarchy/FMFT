using Xeptions;

namespace FMFT.Web.Server.Models.Users.Exceptions
{
    public class RegisterUserWithLoginValidationException : Xeption
    {
        public RegisterUserWithLoginValidationException() 
            : base("Invalid RegisterUserWithLogin model. Please correct the errors and try again.")
        {

        }
    }
}
