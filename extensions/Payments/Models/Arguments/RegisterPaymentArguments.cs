namespace FMFT.Extensions.Payments.Models.Arguments
{
    public class RegisterPaymentArguments
    {
        public int OrderId { get; set; }
        public string SessionId { get; set; }

        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public DateTimeOffset ExpireDate { get; set; }

        public int CustomerId { get; set; }
        public string CustomerFirstName { get; set; }
        public string CustomerLastName { get; set; }
        public string CustomerEmailAddress { get; set; }

        public List<Item> Items { get; set; }

        public class Item
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public decimal Price { get; set; }
            public int Quantity { get; set; }
        }
    }
}
