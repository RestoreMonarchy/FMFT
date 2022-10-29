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
        public Seat SelectedSeat { get; set; }
        [Parameter]
        public EventCallback<Seat> SelectedSeatChanged { get; set; }

        private async ValueTask ChangeSeatAsync(Seat seat)
        {
            SelectedSeat = seat;
            await SelectedSeatChanged.InvokeAsync(seat);
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

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (!firstRender)
                return;

            if (Auditorium == null)
            {
                return;
            }            

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

            if (SelectedSeat != null)
            {
                await DrawSeatAsync(SelectedSeat.Row, SelectedSeat.Number, "orange");
            }
        }

        [JSInvokable]
        public async Task HandleSeatClickAsync(int row, int column)
        {
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

            if (SelectedSeat != null)
            {
                await DrawSeatAsync(SelectedSeat.Row, SelectedSeat.Number, "#009578");

                if (SelectedSeat.Row == row && SelectedSeat.Number == column)
                {
                    await ChangeSeatAsync(null);
                    return;
                }                
            }

            await ChangeSeatAsync(seat);
            await DrawSeatAsync(row, column, "orange");
        }

        private async ValueTask DrawSeatAsync(int row, int column, string color)
        {
            await JSRuntimeBroker.DrawSeatAsync(SeatsCanvasOptions, row, column, color);
        }
    }
}
