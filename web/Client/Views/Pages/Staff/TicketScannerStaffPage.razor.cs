using FMFT.Extensions.Blazor.Bases.Alerts;
using FMFT.Extensions.Blazor.Bases.Javascript;
using FMFT.Extensions.Blazor.Bases.Javascript.Params;
using FMFT.Extensions.Blazor.Bases.Loadings;
using FMFT.Web.Client.Models.API;
using FMFT.Web.Client.Models.API.Reservations;
using FMFT.Web.Client.Models.API.Reservations.Requests;
using FMFT.Web.Client.Models.API.Reservations.Responses;
using FMFT.Web.Client.Models.API.Shows;
using FMFT.Web.Shared.Enums;
using Microsoft.AspNetCore.Components;

namespace FMFT.Web.Client.Views.Pages.Staff
{
    public partial class TicketScannerStaffPage
    {
        [Parameter]
        public int? ShowId { get; set; }

        public string SecretCode { get; set; }

        public LoadingView LoadingView { get; set; }
        public AlertGroupBase AlertGroup { get; set; }
        public AlertBase InvalidInputAlert { get; set; }

        public APIResponse<Show> ShowResponse  { get; set; }
        public APIResponse<ValidateReservationResponse> ValidateReservationResponse { get; set; }

        public Show Show => ShowResponse.Object;
        public int? TicketReservationSeatId => ValidateReservationResponse.Object.ReservationSeatId;
        public ReservationSeat TicketReservationSeat => Reservation.Seats.FirstOrDefault(x => x.Id == TicketReservationSeatId);
        public Reservation Reservation => ValidateReservationResponse.Object.Reservation;

        protected override async Task OnParametersSetAsync()
        {
            ShowResponse = null;
            ValidateReservationResponse = null;

            if (ShowId.HasValue)
            {
                ShowResponse = await APIBroker.GetPublicShowByIdAsync(ShowId.Value);
            }
        }

        protected override void OnInitialized()
        {
            JavascriptEvents.OnKeyPress += HandleKeyPressAsync;
        }

        private string text = string.Empty;

        private async Task HandleKeyPressAsync(KeyPressParams @params)
        {
            if (isLoading)
            {
                return;
            }

            if (@params.Key.Length == 1)
            {
                text += @params.Key;
            }

            // When the pressed key is "Enter"
            if (@params.KeyCode == 13)
            {
                SecretCode = text;
                text = string.Empty;
                await HandleSubmitAsync();
            }            
        }

        private bool isLoading = false;

        private async Task HandleSubmitAsync()
        {
            AlertGroup.HideAll();
            ValidateReservationResponse = null;

            isLoading = true;
            StateHasChanged();

            string secretCodeString = SecretCode.TrimStart(' ').TrimEnd(' ');

            if (!Guid.TryParse(secretCodeString, out Guid secretCode))
            {
                await Task.Delay(500);
                InvalidInputAlert.Show();
            } else
            {
                ValidateReservationRequest request = new()
                {
                    SecretCode = secretCode
                };

                ValidateReservationResponse = await APIBroker.ValidateReservationAsync(request);
            }

            isLoading = false;
            StateHasChanged();
        }

        private string GetSeatClass(ReservationSeat seat)
        {
            if (TicketReservationSeatId.HasValue && TicketReservationSeatId.Value == seat.Id)
            {
                return "fw-bold";
            }

            return string.Empty;
        }

        private string GetInfoClass()
        {
            if (ShowId.HasValue && Reservation.Show.Id != ShowId.Value)
            {
                return "border-warning";
            }

            if (Reservation.Status == ReservationStatus.Ok)
            {
                return "border-success";
            }

            return "border-danger";
        }
    }
}
