using Xeptions;

namespace FMFT.Web.Server.Models.ResetPasswordRequests.Exceptions
{
    public class ResetPasswordValidationException : Xeption
    {
        public ResetPasswordValidationException() : 
            base("ERR025: Reset password validation problem")
        {

        }
    }
}
