namespace FMFT.Web.Server.Models.ShowProducts.Exceptions
{
    public class NotFoundShowProductException : Exception
    {
        public NotFoundShowProductException() 
            : base("ERR040: Show product not found")
        {

        }
    }
}
