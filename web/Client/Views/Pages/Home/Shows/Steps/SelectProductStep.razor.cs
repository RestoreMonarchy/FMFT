using FMFT.Extensions.Blazor.Bases.Navigations;
using FMFT.Extensions.Blazor.Bases.Steppers;
using FMFT.Web.Client.Models.API.Reservations;
using FMFT.Web.Client.Models.API.ShowProducts;
using FMFT.Web.Client.Models.API.Shows;
using FMFT.Web.Client.Models.Services.Orders;
using Microsoft.AspNetCore.Components;

namespace FMFT.Web.Client.Views.Pages.Home.Shows.Steps
{
    public partial class SelectProductStep
    {
        [Parameter]
        public OrderState OrderState { get; set; }
        [Parameter]
        public NavigationStepper Stepper { get; set; }
        [Parameter]
        public Show Show { get; set; }
        [Parameter]
        public List<ShowProduct> ShowProducts { get; set; }
        [Parameter]
        public List<Reservation> UserReservations { get; set; }

        private IEnumerable<Reservation> ActiveUserReservations => UserReservations.Where(x => !x.IsCanceled);

        protected override void OnParametersSet()
        {
            if (OrderState.Items.Count == 0)
            {
                foreach (ShowProduct showProduct in ShowProducts)
                {
                    OrderItemState orderItem = new()
                    {
                        Show = Show,
                        ShowProduct = showProduct,
                        Quantity = 0
                    };

                    OrderState.Items.Add(orderItem);
                }
            }
            
            if (orderItems.Count != OrderState.Items.Count)
            {
                foreach (OrderItemState orderItem in OrderState.Items)
                {
                    orderItems.Add(orderItem.ShowProduct.Id, orderItem);
                }
            }
        }
        
        private Dictionary<int, OrderItemState> orderItems = new();
        private const int MaxQuantity = 5;
        private bool NextDisabled => GetTotalQuantity() == 0;

        private decimal GetTotalPrice()
        {
            decimal totalPrice = 0;
            foreach (OrderItemState orderItem in orderItems.Values)
            {
                totalPrice += orderItem.ShowProduct.Price * orderItem.Quantity;
            }

            return totalPrice;
        }

        private int GetTotalQuantity()
        {
            return orderItems.Values.Sum(x => x.Quantity);
        }

        private async Task HandleShowSelectSeats()
        {
            await StorageBroker.SetOrderStateAsync(Show.Id, OrderState);
            await Stepper.StepUpAsync();
        }
    }
}
