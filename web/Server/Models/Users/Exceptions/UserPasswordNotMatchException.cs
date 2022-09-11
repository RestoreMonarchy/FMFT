namespace FMFT.Web.Server.Models.Users.Exceptions
{
    public class UserPasswordNotMatchException : Exception
    {
        public UserPasswordNotMatchException()
            : base("ERR009: User password not match")
        {

        }
    }
}
