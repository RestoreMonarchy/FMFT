namespace FMFT.Web.Server.Models.ShowProducts.Params
{
    public class AddShowProductParams
    {
        public int ShowId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public short Quantity { get; set; }
        public bool IsBulk { get; set; }
        public bool IsEnabled { get; set; }
    }
}
