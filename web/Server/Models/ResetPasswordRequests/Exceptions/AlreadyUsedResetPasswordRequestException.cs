namespace FMFT.Web.Server.Models.ResetPasswordRequests.Exceptions
{
    public class AlreadyUsedResetPasswordRequestException : Exception
    {
        public AlreadyUsedResetPasswordRequestException()
            : base("ERR026: This reset password request has already been used")
        {

        }
    }
}
