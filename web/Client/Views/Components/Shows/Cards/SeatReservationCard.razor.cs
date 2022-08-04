using FMFT.Web.Client.Services.Views.Shows;
using FMFT.Web.Client.Views.Bases.Alerts;
using FMFT.Web.Client.Views.Bases.Buttons;
using FMFT.Web.Server.Models.Auditoriums;
using FMFT.Web.Server.Models.Reservations.Exceptions;
using FMFT.Web.Server.Models.Reservations.Models;
using FMFT.Web.Server.Models.Seats;
using FMFT.Web.Server.Models.Shows;
using Microsoft.AspNetCore.Components;

namespace FMFT.Web.Client.Views.Components.Shows.Cards
{
    public partial class SeatReservationCard
    {
        [Parameter]
        public Auditorium Auditorium { get; set; }
        [Parameter]
        public Show Show { get; set; }

        [Inject]
        public IShowViewService ShowViewService { get; set; }

        public Seat SelectedSeat { get; set; }

        public AlertGroupBase AlertGroup { get; set; }
        public AlertBase SeatAlreadyReservedAlert { get; set; }
        public AlertBase UserAlreadyReservedAlert { get; set; }

        public ButtonBase ReserveButton { get; set; }

        private async Task HandleReserveButtonClick()
        {
            await PostReserveSeatAsync();
        }

        public async ValueTask PostReserveSeatAsync()
        {
            AlertGroup.HideAll();
            ReserveButton.StartSpinning();

            CreateReservationModel model = new()
            {
                ShowId = Show.Id,
                SeatId = SelectedSeat.Id
            };

            try
            {
                await ShowViewService.CreateReservationAsync(model);
            } catch (SeatAlreadyReservedException)
            {
                SeatAlreadyReservedAlert.Show();
            } catch (UserAlreadyReservedException)
            {
                UserAlreadyReservedAlert.Show();
            }

            ReserveButton.StopSpinning();
        }
    }
}
