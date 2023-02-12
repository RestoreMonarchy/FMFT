using FMFT.Web.Client.Models.API.ShowProducts;
using FMFT.Web.Client.Models.API.Shows;

namespace FMFT.Web.Client.Models.Services.Orders
{
    public class OrderItemState
    {
        public ShowProduct ShowProduct { get; set; }
        public Show Show { get; set; }
        public int Quantity { get; set; }
    }
}
