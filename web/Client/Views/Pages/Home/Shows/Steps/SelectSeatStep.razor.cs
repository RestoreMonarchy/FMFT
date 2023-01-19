using FMFT.Extensions.Blazor.Bases.Steppers;
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
        public Stepper Stepper { get; set; }
        [Parameter]
        public Auditorium Auditorium { get; set; }
        [Parameter]
        public Show Show { get; set; }

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

        private bool HasSelectedSeats => SelectedSeats.Count == TicketsCount;

        public List<Seat> SelectedSeats { get; set; } = new();

        private void BackToSelectProduct()
        {
            Stepper.StepDown();
        }

        private Task NextToConfirm()
        {
            OrderState.SeatIds = SelectedSeats.Select(x => x.Id).ToList();
            Stepper.StepUp();

            return Task.CompletedTask;
        }
    }
}
