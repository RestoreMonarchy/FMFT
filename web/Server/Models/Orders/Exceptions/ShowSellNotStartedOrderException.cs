namespace FMFT.Web.Server.Models.Orders.Exceptions
{
    public class ShowSellNotStartedOrderException : Exception
    {
        public ShowSellNotStartedOrderException()
            : base("ERR053: One or more shows have not started selling")
        {

        }
    }
}
