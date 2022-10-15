namespace FMFT.Web.Client.Models.Accounts.Exceptions
{
    public class AccountValidationException : Exception
    {
        public AccountValidationException(Exception innerException)
            : base(null, innerException)
        {

        }
    }
}
