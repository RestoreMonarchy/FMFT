using FMFT.Web.Client.Models.API.ShowProducts;
using FMFT.Web.Client.Models.API.Shows;

namespace FMFT.Web.Client.Models.API.Orders
{
    public class OrderItem
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public ShowProduct ShowProduct { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public Show Show { get; set; }
    }
}
