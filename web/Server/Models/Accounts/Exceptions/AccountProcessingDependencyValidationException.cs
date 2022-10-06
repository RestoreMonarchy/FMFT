namespace FMFT.Web.Server.Models.Accounts.Exceptions
{
    public class AccountProcessingDependencyValidationException : Exception
    {
        public AccountProcessingDependencyValidationException(Exception innerException)
            : base(null, innerException)
        {

        }
    }
}
