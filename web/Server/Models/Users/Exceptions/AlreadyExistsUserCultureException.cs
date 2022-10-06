using Xeptions;

namespace FMFT.Web.Server.Models.Users.Exceptions
{
    public class AlreadyExistsUserCultureException : Xeption
    {
        public AlreadyExistsUserCultureException()
            : base("ERR011: User already has this culture set")
        {

        }
    }
}
