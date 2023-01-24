using FMFT.Web.Client.Models.API.Shows;

namespace FMFT.Web.Client.Models.Services.Orders
{
    public class OrderStateItemData
    {
        public int ShowProductId { get; set; }
        public int ShowId { get; set; }
        public int Quantity { get; set; }
    }
}
