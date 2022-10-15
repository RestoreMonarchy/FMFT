using FMFT.Web.Client.Models.AccountReservations.Arguments;
using FMFT.Web.Client.Models.Auditoriums;
using FMFT.Web.Client.Models.Reservations.Exceptions;
using FMFT.Web.Client.Models.Reservations.Requests;
using FMFT.Web.Client.Models.Seats;
using FMFT.Web.Client.Models.Shows;
using FMFT.Web.Client.Services.Views.Shows;
using FMFT.Web.Client.Views.Bases.Alerts;
using FMFT.Web.Client.Views.Bases.Buttons;
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
        public AlertBase ReservationSuccessAlert { get; set; }

        public ButtonBase ReserveButton { get; set; }

        private async Task HandleReserveButtonClick()
        {
            await PostReserveSeatAsync();
        }

        public async ValueTask PostReserveSeatAsync()
        {
            AlertGroup.HideAll();
            ReserveButton.StartSpinning();

            CreateAccountReservationArguments arguments = new()
            {
                ShowId = Show.Id,
                SeatId = SelectedSeat.Id
            };

            try
            {
                await ShowViewService.CreateAccountReservationAsync(arguments);
                ReservationSuccessAlert.Show();
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
