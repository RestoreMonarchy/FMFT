namespace FMFT.Web.Server.Models.Accounts.Exceptions
{
    public class AccountProcessingValidationException : Exception
    {
        public AccountProcessingValidationException(Exception innerException)
            : base(null, innerException)
        {

        }
    }
}
