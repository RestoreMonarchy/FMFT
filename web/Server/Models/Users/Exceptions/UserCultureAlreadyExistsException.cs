using Xeptions;

namespace FMFT.Web.Server.Models.Users.Exceptions
{
    public class UserCultureAlreadyExistsException : Xeption
    {
        public UserCultureAlreadyExistsException()
            : base("ERR011: User already has this culture set")
        {

        }
    }
}
