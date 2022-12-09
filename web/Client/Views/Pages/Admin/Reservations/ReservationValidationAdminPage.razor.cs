using FMFT.Extensions.Blazor.Bases.Alerts;
using FMFT.Extensions.Blazor.Bases.Inputs;
using FMFT.Extensions.Blazor.Bases.Javascript;
using FMFT.Extensions.Blazor.Bases.Javascript.Params;
using FMFT.Web.Client.Models.API;
using FMFT.Web.Client.Models.API.Reservations;
using FMFT.Web.Client.Models.API.Reservations.Requests;
using FMFT.Web.Client.Models.API.Reservations.Responses;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace FMFT.Web.Client.Views.Pages.Admin.Reservations
{
    public partial class ReservationValidationAdminPage
    {
        public string SecretCode { get; set; }

        public TextInputBase SearchInput { get; set; }
        public AlertGroupBase AlertGroup { get; set; }
        public AlertBase InvalidInputAlert { get; set; }

        public APIResponse<ValidateReservationResponse> ValidateReservationResponse { get; set; }

        public Reservation Reservation => ValidateReservationResponse.Object.Reservation;

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
    }
}
