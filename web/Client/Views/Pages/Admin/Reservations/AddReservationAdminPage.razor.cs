using FMFT.Extensions.Blazor.Bases.Loadings;
using FMFT.Web.Client.Models.API;
using FMFT.Web.Client.Models.API.Auditoriums;
using FMFT.Web.Client.Models.API.Shows;

namespace FMFT.Web.Client.Views.Pages.Admin.Reservations
{
    public partial class AddReservationAdminPage
    {
        public LoadingView LoadingView { get; set; }

        public APIResponse<List<Show>> ShowsResponse { get; set; }
        public APIResponse<List<Auditorium>> AuditoriumsResponse { get; set; }

        public List<Show> Shows => ShowsResponse.Object;
        public List<Auditorium> Auditorioums => AuditoriumsResponse.Object;

        protected override async Task OnInitializedAsync()
        {
            ShowsResponse = await APIBroker.GetAllShowsAsync();
            AuditoriumsResponse = await APIBroker.GetAllAuditoriumsAsync();
            LoadingView.StopLoading();
        }
    }
}
