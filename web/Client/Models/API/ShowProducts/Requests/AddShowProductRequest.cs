namespace FMFT.Web.Client.Models.API.ShowProducts.Requests
{
    public class AddShowProductRequest
    {
        public int ShowId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public bool IsEnabled { get; set; }
    }
}
