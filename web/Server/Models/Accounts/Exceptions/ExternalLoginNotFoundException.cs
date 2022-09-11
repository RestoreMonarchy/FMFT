namespace FMFT.Web.Server.Models.Accounts.Exceptions
{
    public class ExternalLoginNotFoundException : Exception
    {
        public ExternalLoginNotFoundException()
            : base("ERR003: External login not found")
        {

        }
    }
}
