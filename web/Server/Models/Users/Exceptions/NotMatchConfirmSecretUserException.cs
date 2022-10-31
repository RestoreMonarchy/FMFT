namespace FMFT.Web.Server.Models.Users.Exceptions
{
    public class NotMatchConfirmEmailSecretUserException : Exception
    {
        public NotMatchConfirmEmailSecretUserException()
            : base("ERR022: Secret key and user id do not match")
        {

        }
    }
}
