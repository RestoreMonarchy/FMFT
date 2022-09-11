using Xeptions;

namespace FMFT.Web.Server.Models.Accounts.Exceptions
{
    public class AccountNotAuthenticatedException : Xeption
    {
        public AccountNotAuthenticatedException() 
            : base("ERR001: Account not authenticated")
        {

        }
    }
}
