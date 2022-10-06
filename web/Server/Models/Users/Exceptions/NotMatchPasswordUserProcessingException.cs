namespace FMFT.Web.Server.Models.Users.Exceptions
{
    public class NotMatchPasswordUserProcessingException : Exception
    {
        public NotMatchPasswordUserProcessingException()
            : base("ERR009: Invalid user credentials")
        {

        }
    }
}
