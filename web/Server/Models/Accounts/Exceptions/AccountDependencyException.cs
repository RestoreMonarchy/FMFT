namespace FMFT.Web.Server.Models.Accounts.Exceptions
{
    public class AccountDependencyException : Exception
    {
        public AccountDependencyException(Exception innerException)
            : base(null, innerException)
        {

        }
    }
}
