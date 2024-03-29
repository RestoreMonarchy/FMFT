﻿using FMFT.Web.Client.Models.API.Users;
using FMFT.Web.Shared.Enums;

namespace FMFT.Web.Client.Models.API.Orders
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
        public DateTimeOffset ExpireDate { get; set; }
        public bool IsExpired { get; set; }
        public DateTimeOffset UpdateDate { get; set; }
        public DateTimeOffset CreateDate { get; set; }

        public UserInfo User { get; set; }
        public List<OrderItem> Items { get; set; }
    }
}
