namespace FMFT.Web.Server.Models.Orders.Exceptions
{
    public class ShowDisabledOrderException : Exception
    {
        public ShowDisabledOrderException() 
            : base("ERR052: One or more shows are disabled")
        {

        }
    }
}
