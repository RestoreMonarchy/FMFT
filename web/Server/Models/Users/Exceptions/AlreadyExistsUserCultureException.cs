using Xeptions;

namespace FMFT.Web.Server.Models.Users.Exceptions
{
    public class AlreadyExistsUserCultureException : Exception
    {
        public AlreadyExistsUserCultureException()
            : base("ERR011: User already has this culture set")
        {

        }
    }
}
