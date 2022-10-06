using Xeptions;

namespace FMFT.Web.Server.Models.Accounts.Exceptions
{
    public class NotAuthorizedAccountProcessingException : Xeption
    {
        public NotAuthorizedAccountProcessingException()
            : base("ERR002: Account not authorized")
        {

        }
    }
}
