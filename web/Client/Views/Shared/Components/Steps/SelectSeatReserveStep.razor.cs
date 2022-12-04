using FMFT.Extensions.Blazor.Bases.Steppers;
using FMFT.Web.Client.Models.API.Auditoriums;
using FMFT.Web.Client.Models.API.Seats;
using FMFT.Web.Client.Models.API.Shows;
using Microsoft.AspNetCore.Components;

namespace FMFT.Web.Client.Views.Shared.Components.Steps
{
    public partial class SelectSeatReserveStep
    {
        private const int MaxSeats = 3;

        [CascadingParameter]
        public Stepper Stepper { get; set; }

        [Parameter]
        public Show Show { get; set; }
        [Parameter]
        public Auditorium Auditorium { get; set; }

        [Parameter]
        public List<Seat> SelectedSeats { get; set; }
        [Parameter]
        public EventCallback<List<Seat>> SelectedSeatsChanged { get; set; }


        public int TicketsCount { get; set; } = 1;
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

        private bool showSelectSeats = false;
        private bool HasSelectedSeats => SelectedSeats.Count == TicketsCount;

        private void HandleShowSelectSeats()
        {
            showSelectSeats = true;
        }

        private void HandleHideSelectSeats()
        {
            showSelectSeats = false;
        }

        private Task NextToConfirmAsync()
        {
            Stepper.StepUp();
            return Task.CompletedTask;
        }
    }
}
