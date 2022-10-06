namespace FMFT.Web.Server.Models.Accounts.Exceptions
{
    public class AccountServiceException : Exception
    {
        public AccountServiceException(Exception innerException)
            : base(null, innerException)
        {

        }
    }
}
