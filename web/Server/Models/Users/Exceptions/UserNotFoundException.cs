using Xeptions;

namespace FMFT.Web.Server.Models.Users.Exceptions
{
    public class UserNotFoundException : Xeption
    {
        public UserNotFoundException()
            : base("ERR008: User not found")
        {

        }
    }
}
