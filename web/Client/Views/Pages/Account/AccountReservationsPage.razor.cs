using FMFT.Extensions.Blazor.Bases.Loadings;
using FMFT.Web.Client.Models.API;
using FMFT.Web.Client.Models.API.Reservations;

namespace FMFT.Web.Client.Views.Pages.Account
{
    public partial class AccountReservationsPage
    {
        public LoadingView LoadingView { get; set; }

        public APIResponse<List<Reservation>> ReservationsResponse { get; set; }

        public List<Reservation> Reservations => ReservationsResponse.Object;

        protected override async Task OnInitializedAsync()
        {
            ReservationsResponse = await APIBroker.GetUserReservationsAsync(UserAccountState.UserAccount.UserId);
            LoadingView.StopLoading();
        }
    }
}
