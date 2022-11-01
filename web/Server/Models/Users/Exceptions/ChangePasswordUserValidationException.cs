using Xeptions;

namespace FMFT.Web.Server.Models.Users.Exceptions
{
    public class ChangePasswordUserValidationException : Xeption
    {
        public ChangePasswordUserValidationException()
            : base("ERR024: Change user password validation problem")
        {

        }

    }
}
