using Xeptions;

namespace FMFT.Web.Server.Models.Accounts.Exceptions
{
    public class NotAuthenticatedAccountException : Exception
    {
        public NotAuthenticatedAccountException() 
            : base("ERR001: Account not authenticated")
        {

        }
    }
}
