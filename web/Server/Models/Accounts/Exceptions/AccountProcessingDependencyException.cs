namespace FMFT.Web.Server.Models.Accounts.Exceptions
{
    public class AccountProcessingDependencyException : Exception
    {
        public AccountProcessingDependencyException(Exception innerException)
            : base(null, innerException)
        {

        }
    }
}
