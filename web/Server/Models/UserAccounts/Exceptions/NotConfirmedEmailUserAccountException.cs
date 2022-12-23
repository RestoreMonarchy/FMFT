namespace FMFT.Web.Server.Models.UserAccounts.Exceptions
{
    public class NotConfirmedEmailUserAccountException : Exception
    {
        public NotConfirmedEmailUserAccountException() 
            : base("ERR037: User account does not have a confirmed email")
        {

        }
    }
}
