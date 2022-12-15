using BlazorPanzoom;
using FMFT.Web.Client.Models.API.Auditoriums;
using FMFT.Web.Client.Models.API.Seats;
using FMFT.Web.Client.Models.API.Shows;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Security.Cryptography.X509Certificates;

namespace FMFT.Web.Client.Views.Shared.Components.Panzooms
{
    public partial class AuditoriumSeatPanzoom
    {
        [Parameter]
        public Auditorium Auditorium { get; set; }
        [Parameter]
        public List<ShowReservedSeat> ReservedSeats { get; set; }
        [Parameter]
        public List<Seat> SelectedSeats { get; set; } = new();
        [Parameter]
        public int MaxAmount { get; set; } = 3;
        [Parameter]
        public EventCallback<List<Seat>> SelectedSeatsChanged { get; set; }

        private async ValueTask AddSeatAsync(Seat seat)
        {
            SelectedSeats.Add(seat);
            await SelectedSeatsChanged.InvokeAsync(SelectedSeats);
        }

        private async ValueTask RemoveSeatAsync(Seat seat)
        {
            SelectedSeats.Remove(seat);
            await SelectedSeatsChanged.InvokeAsync(SelectedSeats);
        }

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

        public async Task ReloadAsync()
        {
            DotNetObjectReference<AuditoriumSeatPanzoom> objectReference = DotNetObjectReference.Create(this);
            await JSRuntimeBroker.InitializeSeatsCanvasAsync(SeatsCanvasOptions, objectReference);

            if (ReservedSeats != null)
            {
                foreach (ShowReservedSeat reservedSeat in ReservedSeats)
                {
                    Seat seat = Auditorium.Seats.FirstOrDefault(x => x.Id == reservedSeat.SeatId);
                    if (seat == null)
                    {
                        continue;
                    }

                    if (reservedSeat.IsVip)
                    {
                        await DrawSeatAsync(seat.Row, seat.Number, "#9966cc");
                    }
                    else
                    {
                        await DrawSeatAsync(seat.Row, seat.Number, "dimgray");
                    }
                }
            }

            foreach (Seat seat in SelectedSeats)
            {
                await DrawSeatAsync(seat.Row, seat.Number, "orange");
            }
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (!firstRender)
                return;

            if (Auditorium == null)
            {
                return;
            }

            await ReloadAsync();
        }

        [JSInvokable]
        public async Task HandleSeatClickAsync(int row, int column)
        {
            LoggingBroker.LogDebug($"HandleSeatClickAsync for row {row} and column {column}");

            Seat seat = Auditorium.Seats.FirstOrDefault(x => x.Row == row && x.Number == column);

            if (seat == null)
            {
                LoggingBroker.LogDebug($"Seat at row {row} and column {column} does not exist");
                return;
            }

            if (ReservedSeats.Exists(x => x.SeatId == seat.Id))
            {
                LoggingBroker.LogDebug($"The selected seat id {seat.Id} is already reserved");
                return;
            }

            // uncheck the seat
            if (SelectedSeats.Contains(seat)) 
            {
                LoggingBroker.LogDebug($"Unchecking the seat id {seat.Id} at row {row} and column {column}");

                await DrawSeatAsync(seat.Row, seat.Number, "#009578");

                await RemoveSeatAsync(seat);
                return;
            }

            if (SelectedSeats.Count >= MaxAmount)
            {
                LoggingBroker.LogDebug("You have reached the maximum amount of seats");
                return;
            }

            await AddSeatAsync(seat);
            await DrawSeatAsync(row, column, "orange");
        }

        private async ValueTask DrawSeatAsync(int row, int column, string color)
        {
            await JSRuntimeBroker.DrawSeatAsync(SeatsCanvasOptions, row, column, color);
        }
    }
}
