namespace FMFT.Web.Shared.Models.Users.Exceptions
{
    public class UserPasswordNotMatchException : Exception
    {
        public UserPasswordNotMatchException()
            : base("Invalid email or password")
        {

        }
    }
}
