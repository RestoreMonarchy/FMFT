using FMFT.Web.Server.Models.Users.Exceptions;

namespace FMFT.Web.Server.Models.ResetPasswordRequests.Exceptions
{
    public class NoPasswordUserResetPasswordRequestException : NotPasswordUserException
    {
        public NoPasswordUserResetPasswordRequestException() : base()
        {

        }
    }
}
