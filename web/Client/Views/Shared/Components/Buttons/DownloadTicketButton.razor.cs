using FMFT.Extensions.Blazor.Bases.Buttons;
using FMFT.Web.Client.Models;
using FMFT.Web.Client.Models.API;
using FMFT.Web.Client.Models.API.Reservations;
using Microsoft.AspNetCore.Components;

namespace FMFT.Web.Client.Views.Shared.Components.Buttons
{
    public partial class DownloadTicketButton
    {
        [Parameter]
        public Reservation Reservation { get; set; }
        [Parameter]
        public ReservationItem ReservationItem { get; set; }
        [Parameter]
        public bool IsSmall { get; set; }

        private string GetClass()
        {
            string classes = "btn btn-outline-success";

            if (IsSmall)
            {
                classes += " btn-sm";
            }

            return classes;
        }

        public ButtonBase DownloadButton { get; set; }
        private async Task HandleDownloadTicketAsync()
        {
            DownloadButton.StartSpinning();

            APIResponse<QRCodeImage> response = await APIBroker.GetReservationSeatTicketAsync(Reservation.Id, ReservationItem.Id);

            if (response.IsSuccessful)
            {
                QRCodeImage qrCodeImage = response.Object;
                string fileName = $"{Reservation.Show.Name}-{Reservation.Id}-{ReservationItem.Id}.jpg";

                await JSRuntimeBroker.DownloadFromByteArrayAsync(qrCodeImage.Data, fileName, qrCodeImage.ContentType);
            }

            DownloadButton.StopSpinning();
        }
    }
}
