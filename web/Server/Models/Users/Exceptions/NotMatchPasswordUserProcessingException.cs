namespace FMFT.Web.Server.Models.Users.Exceptions
{
    public class NotMatchPasswordUserException : Exception
    {
        public NotMatchPasswordUserException()
            : base("ERR009: Invalid user credentials")
        {

        }
    }
}
