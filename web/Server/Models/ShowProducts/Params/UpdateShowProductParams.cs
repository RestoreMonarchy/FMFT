namespace FMFT.Web.Server.Models.ShowProducts.Params
{
    public class UpdateShowProductParams
    {
        public int Id { get; set; }
        public int ShowId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public bool IsEnabled { get; set; }
    }
}
