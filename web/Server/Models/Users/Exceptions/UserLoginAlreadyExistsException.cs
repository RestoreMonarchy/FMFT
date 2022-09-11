using Xeptions;

namespace FMFT.Web.Server.Models.Users.Exceptions
{
    public class UserLoginAlreadyExistsException : Xeption
    {
        public UserLoginAlreadyExistsException() 
            : base("ERR007: User with this external login already exists")
        {

        }
    }
}
