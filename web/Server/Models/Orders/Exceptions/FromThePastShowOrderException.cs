namespace FMFT.Web.Server.Models.Orders.Exceptions
{
    public class FromThePastShowOrderException : Exception
    {
        public FromThePastShowOrderException()
            : base("ERR051: One or more shows are from the past")
        {

        }
    }
}
