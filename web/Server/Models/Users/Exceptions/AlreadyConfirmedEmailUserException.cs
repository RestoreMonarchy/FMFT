namespace FMFT.Web.Server.Models.Users.Exceptions
{
    public class AlreadyConfirmedEmailUserException : Exception
    {
        public AlreadyConfirmedEmailUserException()
            : base("ERR021: This user has already confirmed email")
        {

        }
    }
}
