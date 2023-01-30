using FMFT.Web.Server.Models.Orders;
using FMFT.Web.Server.Models.Payments.Params;

namespace FMFT.Web.Server.Services.Coordinations.Orders
{
    public partial class OrderCoordinationService
    {
        public RegisterPaymentParams MapOrderToRegisterPaymentParams(Order order)
        {
            RegisterPaymentParams @params = new()
            {
                PaymentMethod = order.PaymentMethod,
                OrderId = order.Id,
                SessionId = order.SessionId.ToString(),
                Amount = order.Amount,
                Currency = order.Currency,
                ExpireDate = order.ExpireDate,
                CustomerId = order.User.Id,
                CustomerEmailAddress = order.User.Email,
                CustomerFirstName = order.User.FirstName,
                CustomerLastName = order.User.LastName,
                Items = new()
            };

            foreach (OrderItem item in order.Items)
            {
                @params.Items.Add(new RegisterPaymentParams.Item()
                {
                    Id = item.Id,
                    Name = item.ShowProduct.Name,
                    Price = item.Price,
                    Quantity = item.Quantity
                });
            }

            return @params;
        }

        public GetPaymentUrlParams MapOrderToGetPaymentUrlParams(Order order)
        {
            return new GetPaymentUrlParams()
            {
                PaymentMethod = order.PaymentMethod,
                SessionId = order.SessionId.ToString(),
                PaymentToken= order.PaymentToken
            };
        }
    }
}
