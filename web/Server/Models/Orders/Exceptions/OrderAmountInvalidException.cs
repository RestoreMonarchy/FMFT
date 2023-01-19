namespace FMFT.Web.Server.Models.Orders.Exceptions
{
    public class OrderAmountInvalidException : Exception
    {
        public OrderAmountInvalidException() : base("ERR045: Order amount must be greater than 0") { }
    }
}
