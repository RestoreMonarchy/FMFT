using FMFT.Web.Server.Models.Users.Exceptions;

namespace FMFT.Web.Server.Models.ResetPasswordRequests.Exceptions
{
    public class UserNotFoundResetPasswordRequestException : NotFoundUserException
    {
        public UserNotFoundResetPasswordRequestException() : base()
        {

        }
    }
}
