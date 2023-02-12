namespace FMFT.Web.Server.Models.Orders.Exceptions
{
    public class OrderAmountMismatchException : Exception
    {
        public OrderAmountMismatchException() : base("ERR046: Order amount does not match order amount calculated as sum of items") { }
    }
}
