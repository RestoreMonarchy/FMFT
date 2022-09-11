using Xeptions;

namespace FMFT.Web.Server.Models.Accounts.Exceptions
{
    public class AccountNotAuthorizedException : Xeption
    {
        public AccountNotAuthorizedException()
            : base("ERR002: Account not authorized")
        {

        }
    }
}
