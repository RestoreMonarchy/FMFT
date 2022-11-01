namespace FMFT.Web.Server.Models.Users.Exceptions
{
    public class NotPasswordUserException : Exception
    {
        public NotPasswordUserException()
            : base("ERR023: This user does not use password authentication")
        {

        }
    }
}
