namespace FMFT.Web.Server.Models.Orders.Exceptions
{
    public class NotFoundOrderException : Exception
    {
        public NotFoundOrderException() : base("ERR041: Order not found")
        { 

        }
    }
}
