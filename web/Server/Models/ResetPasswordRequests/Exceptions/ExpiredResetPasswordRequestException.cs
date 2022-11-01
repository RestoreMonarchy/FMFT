namespace FMFT.Web.Server.Models.ResetPasswordRequests.Exceptions
{
    public class ExpiredResetPasswordRequestException : Exception
    {
        public ExpiredResetPasswordRequestException() 
            : base("ERR027: This reset password request has expired")
        {

        }
    }
}
