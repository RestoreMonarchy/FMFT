using FMFT.Web.Server.Models.ShowProducts;
using FMFT.Web.Server.Models.Shows;

namespace FMFT.Web.Server.Models.Orders
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
