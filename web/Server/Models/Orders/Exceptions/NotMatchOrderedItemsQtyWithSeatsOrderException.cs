namespace FMFT.Web.Server.Models.Orders.Exceptions
{
    public class NotMatchOrderedItemsQtyWithSeatsOrderException : Exception
    {
        public NotMatchOrderedItemsQtyWithSeatsOrderException() : base("ERR043: Sum of order quantity does not match reserved sets count")
        {

        }
    }
}
