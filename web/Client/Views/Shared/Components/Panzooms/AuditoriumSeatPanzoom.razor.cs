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

        public object SeatsCanvasOptions => new
        {
            canvasId = "myCanvas",
            seatsMap = RowSeats.Select(x => x.Count()).ToArray(),
            marginX = 30,
            marginY = 30,
            sizeX = 25,
            sizeY = 25,
            defaultColor = "#009578",
            font = "bold 12px Arial"
        };
        
        public int Width => RowSeats.Max(x => x.Count()) * 45 + 300;
        public int Height => RowSeats.Count() * 50 + 300;

        private object seatsCanvasObject = null;

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (!firstRender)
                return;

            if (Auditorium == null)
            {
                return;
            }            

            DotNetObjectReference<AuditoriumSeatPanzoom> objectReference = DotNetObjectReference.Create(this);
            seatsCanvasObject = await JSRuntimeBroker.InitializeSeatsCanvasAsync(SeatsCanvasOptions, objectReference);

            if (selectedSeat != null)
            {
                await DrawSeatAsync(selectedSeat.Row, selectedSeat.Number, "orange");
            }
        }

        private Seat selectedSeat = null;

        [JSInvokable]
        public async Task HandleSeatClickAsync(int row, int column)
        {
            Seat seat = Auditorium.Seats.FirstOrDefault(x => x.Row == row && x.Number == column);

            Console.WriteLine($"row: {row} column: {column}");

            if (seat == null)
            {
                Console.WriteLine("seat not found");
                return;
            }

            Console.WriteLine($"Seat ID: {seat.Id}");

            if (selectedSeat != null)
            {
                await DrawSeatAsync(selectedSeat.Row, selectedSeat.Number, "#009578");

                if (selectedSeat.Row == row && selectedSeat.Number == column)
                {
                    selectedSeat = null;
                    return;
                }                
            }

            selectedSeat = seat;
            await DrawSeatAsync(row, column, "orange");
        }

        private async ValueTask DrawSeatAsync(int row, int column, string color)
        {
            await JSRuntimeBroker.DrawSeatAsync(SeatsCanvasOptions, row, column, color);
        }
    }
}
