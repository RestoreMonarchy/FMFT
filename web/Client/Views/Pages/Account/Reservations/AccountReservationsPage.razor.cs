using FMFT.Extensions.Blazor.Bases.Loadings;
using FMFT.Web.Client.Models.API;
using FMFT.Web.Client.Models.API.Reservations;
using FMFT.Web.Shared.Enums;

namespace FMFT.Web.Client.Views.Pages.Account.Reservations
{
    public partial class AccountReservationsPage
    {
        public LoadingView LoadingView { get; set; }

        public APIResponse<List<Reservation>> ReservationsResponse { get; set; }

        public List<Reservation> Reservations => ReservationsResponse.Object;

        public IEnumerable<Reservation> ValidReservations 
            => Reservations.Where(x => x.Status is ReservationStatus.Ok or ReservationStatus.Canceled);
        
        protected override async Task OnInitializedAsync()
        {
            if (!UserAccountState.IsAuthenticated)
            {
                return;
            }

            ReservationsResponse = await APIBroker.GetUserReservationsAsync(UserAccountState.UserAccount.UserId);
            LoadingView.StopLoading();
        }

        public string GetReservationClasses(Reservation reservation)
        {
            List<string> classes = new();

            if (reservation.IsNotValid)
            {
                classes.Add("list-group-item-light");
            }

            return string.Join(", ", classes);
        }
    }
}
