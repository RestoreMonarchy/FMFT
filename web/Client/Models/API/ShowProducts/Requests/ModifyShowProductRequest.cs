namespace FMFT.Web.Client.Models.API.ShowProducts.Requests
{
    public class ModifyShowProductRequest
    {
        public int Id { get; set; }
        public int ShowId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public bool IsBulk { get; set; }
        public short Quantity { get; set; }
        public bool IsEnabled { get; set; }
    }
}
