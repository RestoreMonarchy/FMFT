using Xeptions;

namespace FMFT.Web.Server.Models.Users.Exceptions
{
    public class NotFoundUserException : Xeption
    {
        public NotFoundUserException()
            : base("ERR008: User not found")
        {

        }
    }
}
