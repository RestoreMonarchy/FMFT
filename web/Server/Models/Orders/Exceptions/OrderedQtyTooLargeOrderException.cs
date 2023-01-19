using Hangfire.Server;
using System;

namespace FMFT.Web.Server.Models.Orders.Exceptions
{
    public class OrderedQtyTooLargeOrderException : Exception
    {
        public OrderedQtyTooLargeOrderException() : base("ERR043: Sum of order quantity is too big. It must not exceed 100 for a single order") 
        {
            
        }
    }
}
