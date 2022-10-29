using FMFT.Extensions.Blazor.Bases.Loadings;
using FMFT.Web.Client.Models.API;
using FMFT.Web.Client.Models.API.Reservations;
using Microsoft.AspNetCore.Components;

namespace FMFT.Web.Client.Views.Pages.Account
{
    public partial class AccountReservationPage
    {
        [Parameter]
        public string ReservationId { get; set; }

        public LoadingView LoadingView { get; set; }

        public APIResponse<Reservation> ReservationResponse { get; set; }

        public Reservation Reservation => ReservationResponse.Object;

        protected override async Task OnParametersSetAsync()
        {
            ReservationResponse = await APIBroker.GetReservationByIdAsync(ReservationId);
            LoadingView.StopLoading();
        }
    }
}
