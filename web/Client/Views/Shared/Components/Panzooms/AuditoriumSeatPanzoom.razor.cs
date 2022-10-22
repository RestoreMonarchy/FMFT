using BlazorPanzoom;
using FMFT.Web.Client.Models.API.Auditoriums;
using FMFT.Web.Client.Models.API.Seats;
using Microsoft.AspNetCore.Components;

namespace FMFT.Web.Client.Views.Shared.Components.Panzooms
{
    public partial class AuditoriumSeatPanzoom
    {
        [Parameter]
        public Auditorium Auditorium { get; set; }

        public IEnumerable<IGrouping<short, Seat>> RowSeats 
            => Auditorium.Seats.GroupBy(x => x.Row);

        public Panzoom Panzoom { get; set; }
        public PanzoomOptions PanzoomOptions { get; set; } = new()
        {
            Canvas = true,
            MinScale = 0.5,
            MaxScale = 2
        };

        public int Width => RowSeats.Max(x => x.Count()) * 45 + 300;
        public int Height => RowSeats.Count() * 50 + 300;

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await JSRuntimeBroker.BuildSeatsCanvas("myCanvas");
        }
    }
}
