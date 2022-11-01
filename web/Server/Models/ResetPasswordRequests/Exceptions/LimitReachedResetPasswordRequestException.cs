namespace FMFT.Web.Server.Models.ResetPasswordRequests.Exceptions
{
    public class LimitReachedResetPasswordRequestException : Exception
    {
        public LimitReachedResetPasswordRequestException()
            : base("ERR028: You have reached the limit of reset password requests. Try again later")
        {

        }
    }
}
