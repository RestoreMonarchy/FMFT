namespace FMFT.Web.Server.Models.Accounts.Exceptions
{
    public class AccountProcessingServiceException : Exception
    {
        public AccountProcessingServiceException(Exception innerException)
            : base(null, innerException)
        {

        }
    }
}
