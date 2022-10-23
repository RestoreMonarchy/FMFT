using BlazorPanzoom;
using FMFT.Web.Client.Models.API.Auditoriums;
using FMFT.Web.Client.Models.API.Seats;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

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
            Canvas = false,
            MinScale = 0.5,
            MaxScale = 2
        };

        public int Width => RowSeats.Max(x => x.Count()) * 45 + 300;
        public int Height => RowSeats.Count() * 50 + 300;

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (Auditorium == null)
            {
                return;
            }

            int[] seatsMap = RowSeats.Select(x => x.Count()).ToArray();

            object options = new
            {
                marginX = 30,
                marginY = 30,
                sizeX = 30,
                sizeY = 30,
                defaultColor = "#009578"
            };

            DotNetObjectReference<AuditoriumSeatPanzoom> objectReference = DotNetObjectReference.Create(this);
            await JSRuntimeBroker.BuildSeatsCanvas("myCanvas", seatsMap, options, objectReference);
        }

        [JSInvokable]
        public async Task<string> HandleSeatClickAsync(int row, int column)
        {
            Console.WriteLine("helo there row: {0}, col: {1}", row, column);
            return "orange";
        }
    }
}
