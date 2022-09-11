using Xeptions;

namespace FMFT.Web.Server.Models.Users.Exceptions
{
    public class RegisterUserWithLoginValidationException : Xeption
    {
        public RegisterUserWithLoginValidationException() 
            : base("ERR004: Register with login validation problem")
        {

        }
    }
}
