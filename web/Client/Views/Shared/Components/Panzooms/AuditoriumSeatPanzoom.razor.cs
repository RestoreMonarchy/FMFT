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
        public IEnumerable<ShowReservedItem> ReservedSeats { get; set; }
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

        public Panzoom Panzoom { get; set; }
        public PanzoomOptions PanzoomOptions { get; set; } = new()
        {
            Canvas = false,
            MinScale = 0.5,
            MaxScale = 2
        };

        private int[][] GetSeatsMap()
        {
            IEnumerable<IGrouping<char, Seat>> sectorGroups = Auditorium.Seats
                .GroupBy(x => x.Sector)
                .OrderBy(x => x.Key);

            List<int[]> seatmap = new(); 
            foreach (IGrouping<char, Seat> sectorGroup in sectorGroups)
            {
                IEnumerable<IGrouping<short, Seat>> rowGroups = sectorGroup.GroupBy(x => x.Row);
                seatmap.Add(rowGroups.Select(x => x.Count()).ToArray());
            }

            return seatmap.ToArray();
        }

        private int[][] GetBreakpoints()
        {
            return new int[][]
            {
                Array.Empty<int>(),
                new int[]
                {
                    17,
                    16
                }
            };
        }

        public object SeatsCanvasOptions => new
        {
            canvasId = "myCanvas",
            seatsMap = GetSeatsMap(),
            marginX = 30,
            marginY = 30,
            sizeX = 25,
            sizeY = 25,
            defaultColor = "#009578",
            font = "bold 12px Arial",
            stageWidth = 400,
            stageHeight = 40,
            stageOffset = 50,
            stageFont = "bold 14px Arial",
            stageColor = "#D51360",
            sectorSpace = 15,
            breakpointSpace = 70,
            breakpoints = GetBreakpoints()
        };

        public async Task ReloadAsync()
        {
            DotNetObjectReference<AuditoriumSeatPanzoom> objectReference = DotNetObjectReference.Create(this);
            await JSRuntimeBroker.InitializeSeatsCanvasAsync(SeatsCanvasOptions, objectReference);

            if (ReservedSeats != null)
            {
                foreach (ShowReservedItem reservedSeat in ReservedSeats)
                {
                    Seat seat = Auditorium.Seats.FirstOrDefault(x => x.Id == reservedSeat.SeatId);
                    if (seat == null)
                    {
                        continue;
                    }

                    await DrawSeatAsync(seat.Row, seat.Number, seat.Sector, "dimgray");
                }
            }

            foreach (Seat seat in SelectedSeats.ToList())
            {
                if (ReservedSeats.Any(x => x.SeatId == seat.Id))
                {
                    await RemoveSeatAsync(seat);
                    break;
                }

                await DrawSeatAsync(seat.Row, seat.Number, seat.Sector, "orange");
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
        public async Task HandleSeatClickAsync(int row, int column, int sector)
        {
            LoggingBroker.LogDebug($"HandleSeatClickAsync for row {row} and column {column}");

            char sectorChar = sector == 1 ? 'A' : 'B';

            Seat seat = Auditorium.Seats.FirstOrDefault(x => x.Row == row && x.Number == column && x.Sector == sectorChar);

            if (seat == null)
            {
                LoggingBroker.LogDebug($"Seat at row {row} and column {column} does not exist");
                return;
            }

            if (ReservedSeats.Any(x => x.SeatId == seat.Id))
            {
                LoggingBroker.LogDebug($"The selected seat id {seat.Id} is already reserved");
                return;
            }

            // uncheck the seat
            if (SelectedSeats.Contains(seat)) 
            {
                LoggingBroker.LogDebug($"Unchecking the seat id {seat.Id} at row {row} and column {column}");

                await DrawSeatAsync(seat.Row, seat.Number, seat.Sector, "#009578");

                await RemoveSeatAsync(seat);
                return;
            }

            if (SelectedSeats.Count >= MaxAmount)
            {
                LoggingBroker.LogDebug("You have reached the maximum amount of seats");
                return;
            }

            await AddSeatAsync(seat);
            await DrawSeatAsync(row, column, sectorChar, "orange");
        }

        private async ValueTask DrawSeatAsync(int row, int column, char sector, string color)
        {
            int sectorNum = sector == 'A' ? 1 : 2;

            await JSRuntimeBroker.DrawSeatAsync(SeatsCanvasOptions, row, column, sectorNum, color);
        }

        private async Task HandleZoomInAsync()
        {
            await Panzoom.ZoomInAsync();
        }

        private async Task HandleZoomOutAsync()
        {
            await Panzoom.ZoomOutAsync();
        }

        private async Task HandleResetAsync()
        {
            await Panzoom.ResetAsync();
        }

        private async Task HandleOnWheelAsync(CustomWheelEventArgs args)
        {
            if (!args.ShiftKey)
            {
                return;
            }
            await Panzoom.ZoomWithWheelAsync(args);
        }
    }
}
