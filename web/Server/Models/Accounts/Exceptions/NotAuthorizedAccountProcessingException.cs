namespace FMFT.Web.Server.Models.Accounts.Exceptions
{
    public class NotAuthorizedAccountException : Exception
    {
        public NotAuthorizedAccountException()
            : base("ERR002: Account not authorized")
        {

        }
    }
}
