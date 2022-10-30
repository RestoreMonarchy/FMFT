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

        private void GoToReservation(Reservation reservation)
        {
            NavigationBroker.NavigateTo($"/account/reservations/{reservation.Id}");
        }

        public string GetClasses(Reservation reservation)
        {
            List<string> classes = new();

            if (reservation.IsCanceled)
            {
                classes.Add("list-group-item-light");
            } else
            {
                classes.Add("");
            }

            return string.Join(", ", classes);
        }
    }
}
