namespace FMFT.Web.Server.Models.Accounts.Exceptions
{
    public class AccountDependencyValidationException : Exception
    {
        public AccountDependencyValidationException(Exception innerException)
            : base(null, innerException)
        {

        }
    }
}
