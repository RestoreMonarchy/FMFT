namespace FMFT.Web.Server.Models.UserAccounts.Exceptions
{
    public class LimitConfirmEmailUserAccountException : Exception
    {
        public LimitConfirmEmailUserAccountException()
            : base("ERR039: You have already requested a confirm account link in the last 5 minutes. Try again later")
        {

        }
    }
}
