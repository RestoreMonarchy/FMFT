using FMFT.Extensions.Exceptions.Attributes;
using Xeptions;

namespace FMFT.Web.Server.Models.Users.Exceptions
{
    [WrapWithServiceValidationException]
    public class RegisterUserWithLoginValidationException : Xeption
    {
        public RegisterUserWithLoginValidationException() 
            : base("ERR004: Register with login validation problem")
        {

        }
    }
}
