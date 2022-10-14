namespace FMFT.Web.Client.Models.Accounts.Exceptions
{
    public class AccountDependencyValidationException : Exception
    {
        public AccountDependencyValidationException(Exception innerException)
            : base(null, innerException)
        {

        }
    }
}
