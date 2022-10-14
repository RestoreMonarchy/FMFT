namespace FMFT.Web.Client.Models.Accounts.Exceptions
{
    public class AccountServiceException : Exception
    {
        public AccountServiceException(Exception innerException)
            : base(null, innerException)
        {

        }
    }
}
