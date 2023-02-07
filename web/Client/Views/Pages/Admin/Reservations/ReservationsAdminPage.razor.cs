using FMFT.Extensions.Blazor.Bases.Loadings;
using FMFT.Web.Client.Models.API;
using FMFT.Web.Client.Models.API.Reservations;
using FMFT.Web.Shared.Enums;
using Microsoft.AspNetCore.Components;

namespace FMFT.Web.Client.Views.Pages.Admin.Reservations
{
    public partial class ReservationsAdminPage
    {
        public LoadingView LoadingView { get; set; }

        public APIResponse<List<Reservation>> ReservationsResponse { get; set; }

        public List<Reservation> Reservations => ReservationsResponse.Object;

        private string searchString = string.Empty;

        private IEnumerable<Reservation> SearchReservations => FilterReservations
            .Where(x => string.IsNullOrEmpty(searchString) 
                || x.Show.Name.Contains(searchString, StringComparison.OrdinalIgnoreCase)
                || x.Id.Equals(searchString, StringComparison.OrdinalIgnoreCase));

        private IEnumerable<Reservation> FilterReservations => Reservations
            .Where(x => StatusFilters[x.Status])
            .OrderByDescending(x => x.CreateDate);

        private Dictionary<ReservationStatus, bool> StatusFilters = new()
        {
            { ReservationStatus.Pending, true },
            { ReservationStatus.Ok, true },
            { ReservationStatus.Expired, false },
            { ReservationStatus.Canceled, true }
        };

        protected override async Task OnInitializedAsync()
        {
            ReservationsResponse = await APIBroker.GetAllReservationsAsync();

            LoadingView.StopLoading();
        }

        private string StatusFilterId(ReservationStatus status)
        {
            return $"statusfilter-{status}";
        }

        private void ChangeStatusFilter(ReservationStatus status, ChangeEventArgs args)
        {
            bool value = bool.Parse(args.Value.ToString());
            StatusFilters[status] = value;
            StateHasChanged();
        }

        private string GetClass(Reservation reservation)
        {
            List<string> classes = new();

            switch (reservation.Status)
            {
                case ReservationStatus.Pending:
                    break;
                case ReservationStatus.Ok:
                    classes.Add("table-success");
                    break;
                case ReservationStatus.Expired:
                    classes.Add("table-secondary");
                    break;
                case ReservationStatus.Canceled:
                    classes.Add("table-warning");
                    break;
            }

            return string.Join(' ', classes);
        }
    }
}
