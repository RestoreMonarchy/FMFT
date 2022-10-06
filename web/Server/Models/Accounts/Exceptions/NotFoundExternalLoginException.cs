namespace FMFT.Web.Server.Models.Accounts.Exceptions
{
    public class NotFoundExternalLoginException : Exception
    {
        public NotFoundExternalLoginException()
            : base("ERR003: External login not found")
        {

        }
    }
}
