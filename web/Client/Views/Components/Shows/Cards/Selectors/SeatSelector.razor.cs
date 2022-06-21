using FMFT.Web.Client.Brokers.JSRuntimes;
using FMFT.Web.Client.Views.Bases.Buttons;
using FMFT.Web.Shared.Models.Auditoriums;
using FMFT.Web.Shared.Models.Seats;
using Microsoft.AspNetCore.Components;

namespace FMFT.Web.Client.Views.Components.Shows.Cards.Selectors
{
    public partial class SeatSelector
    {
        [Parameter]
        public Auditorium Auditorium { get; set; }
        [Parameter]
        public List<Seat> SelectedSeats { get; set; } = new();
        [Parameter]
        public EventCallback<List<Seat>> SelectedSeatsChanged { get; set; }

        [Inject]
        public IJSRuntimeBroker JSRuntimeBroker { get; set; }

        public ElementReference ContainerElement { get; set; }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await JSRuntimeBroker.InitializePanzoomElementAsync(ContainerElement);
            }
        }

        public IEnumerable<IGrouping<short, Seat>> RowSeats
            => Auditorium.Seats.OrderBy(x => x.Number).
                GroupBy(x => x.Row);

        private Dictionary<int, ButtonBase> seatButtons = new();

        public void HandleClickSeat(Seat seat)
        {
            if (SelectedSeats.Contains(seat))
            {
                SelectedSeats.Remove(seat);
                seatButtons[seat.Id].RemoveClass("seat-selected");
                InvokeAsync(() => SelectedSeatsChanged.InvokeAsync(SelectedSeats));
            }
            else
            {
                SelectedSeats.Add(seat);
                seatButtons[seat.Id].AddClass("seat-selected");
                InvokeAsync(() => SelectedSeatsChanged.InvokeAsync(SelectedSeats));
            }
        }
    }
}
