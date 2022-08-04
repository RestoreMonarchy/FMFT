namespace FMFT.Web.Server.Models.Users.Exceptions
{
    public class UserPasswordNotMatchException : Exception
    {
        public UserPasswordNotMatchException()
            : base("Invalid email or password")
        {

        }
    }
}
