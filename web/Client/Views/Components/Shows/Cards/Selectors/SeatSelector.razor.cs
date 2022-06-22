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
        public Seat SelectedSeat { get; set; }
        [Parameter]
        public EventCallback<Seat> SelectedSeatChanged { get; set; }

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
            if (SelectedSeat != null)
                seatButtons[SelectedSeat.Id].RemoveClass("seat-selected");

            if (SelectedSeat == seat)
            {
                SelectedSeat = null;
            }
            else
            {
                SelectedSeat = seat;
                seatButtons[seat.Id].AddClass("seat-selected");
            }

            InvokeAsync(() => SelectedSeatChanged.InvokeAsync(SelectedSeat));
        }
    }
}
