namespace FMFT.Web.Server.Models.Orders.Exceptions
{
    public class InvalidShowProductIdOrderException : Exception
    {
        public InvalidShowProductIdOrderException() : base("ERR044: Invalid value of ShowProductId") { }
    }
}
