using Xeptions;

namespace FMFT.Web.Server.Models.Users.Exceptions
{
    public class AlreadyExistsEmailUserException : Exception
    {
        public AlreadyExistsEmailUserException() 
            : base("ERR006: User with this email already exists")
        {

        }
    }
}
