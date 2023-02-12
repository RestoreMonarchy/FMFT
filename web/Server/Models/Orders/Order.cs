using FMFT.Web.Server.Models.Users;
using FMFT.Web.Shared.Enums;

namespace FMFT.Web.Server.Models.Orders
{
    public class Order
    {
        public int Id { get; set; }
        public OrderStatus Status { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public PaymentProvider PaymentProvider { get; set; }
        public Guid SessionId { get; set; }
        public string PaymentToken { get; set; }
        public DateTime ExpireDate { get; set; }
        public bool IsExpired { get; set; }
        public DateTime UpdateDate { get; set; }
        public DateTime CreateDate { get; set; }

        public UserInfo User { get; set; }
        public List<OrderItem> Items { get; set;}
        public int UserId() => User?.Id ?? 0;
    }
}
