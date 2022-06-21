using FMFT.Web.Client.Brokers.JSRuntimes;
using FMFT.Web.Shared.Models.Auditoriums;
using FMFT.Web.Shared.Models.Seats;
using Microsoft.AspNetCore.Components;

namespace FMFT.Web.Client.Views.Components.Shows.Selectors
{
    public partial class SeatSelector
    {
        [Parameter]
        public Auditorium Auditorium { get; set; }

        public IEnumerable<IGrouping<short, Seat>> RowSeats 
            => Auditorium.Seats.OrderBy(x => x.Number).
            GroupBy(x => x.Row);

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
    }
}
