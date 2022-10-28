using Xeptions;

namespace FMFT.Web.Server.Models.Users.Exceptions
{
    public class NotFoundUserException : Exception
    {
        public NotFoundUserException()
            : base("ERR008: User not found")
        {

        }
    }
}
