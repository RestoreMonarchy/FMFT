using FMFT.Extensions.Blazor.Bases.Navigations;
using FMFT.Web.Client.Models.API.Auditoriums;
using FMFT.Web.Client.Models.API.Seats;
using FMFT.Web.Client.Models.API.Shows;
using FMFT.Web.Client.Models.Services.Orders;
using Microsoft.AspNetCore.Components;

namespace FMFT.Web.Client.Views.Pages.Home.Shows.Steps
{
    public partial class SelectSeatStep
    {
        [Parameter]
        public OrderState OrderState { get; set; }
        [Parameter]
        public EventCallback<OrderState> OrderStateChanged { get; set; }
        [Parameter]
        public NavigationStepper Stepper { get; set; }
        [Parameter]
        public Auditorium Auditorium { get; set; }
        [Parameter]
        public Show Show { get; set; }

        private async Task InvokeOrderStateChangedAsync() 
        {
            await OrderStateChanged.InvokeAsync(OrderState);
        }

        private async Task HandleSelectedSeatsChangedAsync(List<Seat> seats)
        {
            OrderState.Seats = seats;
            await InvokeOrderStateChangedAsync();
        }

        protected override async Task OnParametersSetAsync()
        {
            int ticketsCount = TicketsCount;

            if (ticketsCount < OrderState.Seats.Count)
            {
                OrderState.Seats.Clear();
                await InvokeOrderStateChangedAsync();
            }

            if (ticketsCount == 0)
            {
                await BackToSelectProductAsync();
            }
        }

        public int TicketsCount => OrderState.Items.Sum(x => x.Quantity);
        private string TicketsCountString()
        {
            string format;
            if (TicketsCount == 1)
            {
                format = "{0} miejsce";
            }
            else if (TicketsCount > 1 && TicketsCount < 5)
            {
                format = "{0} miejsca";
            }
            else
            {
                format = "{0} miejsc";
            }

            return string.Format(format, TicketsCount);
        }

        private bool HasSelectedSeats => OrderState.Seats.Count == TicketsCount;

        private async Task BackToSelectProductAsync()
        {
            await Stepper.StepDownAsync();
        }

        private async Task NextToConfirm()
        {
            await InvokeOrderStateChangedAsync();
            await Stepper.StepUpAsync();
        }
    }
}
