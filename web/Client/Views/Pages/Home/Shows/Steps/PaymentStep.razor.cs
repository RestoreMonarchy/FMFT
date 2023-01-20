using FMFT.Web.Client.Models.API;
using FMFT.Web.Client.Models.API.Orders;
using FMFT.Web.Client.Models.API.Orders.Requests;
using FMFT.Web.Client.Models.API.ShowProducts;
using FMFT.Web.Client.Models.API.Shows;
using FMFT.Web.Client.Models.Services.Orders;
using Microsoft.AspNetCore.Components;

namespace FMFT.Web.Client.Views.Pages.Home.Shows.Steps
{
    public partial class PaymentStep
    {
        [Parameter]
        public OrderState OrderState { get; set; }
        [Parameter]
        public Show Show { get; set; }

        private Order order;

        public IEnumerable<OrderItemState> OrderItems => OrderState.Items.Where(x => x.Quantity > 0);

        private async Task CreateOrderTmp()
        {
            CreateOrderRequest createOrderRequest = new()
            {
                Amount = OrderState.TotalPrice,
                UserId = UserAccountState.UserAccount.UserId,
                Currency = "PLN",
                PaymentMethod = OrderState.PaymentMethod,   
                SeatIds = OrderState.Seats.Select(x => x.Id).ToList(),
                Items = new()
            };

            foreach (OrderItemState orderItem in OrderState.Items)
            {
                if (orderItem.Quantity == 0)
                {
                    continue;
                }

                createOrderRequest.Items.Add(new CreateOrderRequest.Item()
                {
                    ShowProductId = orderItem.ShowProduct.Id,
                    Price = orderItem.ShowProduct.Price,
                    Quantity = orderItem.Quantity
                });
            }

            Console.WriteLine(createOrderRequest);

            APIResponse<Order> response = await APIBroker.CreateOrderAsync(createOrderRequest);
        }
    }
}
