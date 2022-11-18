using FMFT.Extensions.Blazor.Bases.Loadings;
using FMFT.Web.Client.Models.API;
using FMFT.Web.Client.Models.API.Reservations;

namespace FMFT.Web.Client.Views.Pages.Admin.Reservations
{
    public partial class ReservationsAdminPage
    {
        public LoadingView LoadingView { get; set; }

        public APIResponse<List<Reservation>> ReservationsResponse { get; set; }

        public List<Reservation> Reservations => ReservationsResponse.Object;

        private string searchString = string.Empty;

        private IEnumerable<Reservation> SearchReservations => Reservations
            .Where(x => string.IsNullOrEmpty(searchString) 
                || x.Show.Name.Contains(searchString, StringComparison.OrdinalIgnoreCase)
                || x.Id.Equals(searchString, StringComparison.OrdinalIgnoreCase));

        protected override async Task OnInitializedAsync()
        {
            ReservationsResponse = await APIBroker.GetAllReservationsAsync();

            LoadingView.StopLoading();
        }

    }
}
