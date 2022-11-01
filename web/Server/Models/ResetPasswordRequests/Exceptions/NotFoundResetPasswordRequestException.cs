namespace FMFT.Web.Server.Models.ResetPasswordRequests.Exceptions
{
    public class NotFoundResetPasswordRequestException : Exception
    {
        public NotFoundResetPasswordRequestException() 
            : base("ERR029: Reset password request not found")
        {

        }
    }
}
