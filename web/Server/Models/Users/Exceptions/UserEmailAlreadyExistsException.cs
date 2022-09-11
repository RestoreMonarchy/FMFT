using Xeptions;

namespace FMFT.Web.Server.Models.Users.Exceptions
{
    public class UserEmailAlreadyExistsException : Xeption
    {
        public UserEmailAlreadyExistsException() 
            : base("ERR006: User with this email already exists")
        {

        }
    }
}
