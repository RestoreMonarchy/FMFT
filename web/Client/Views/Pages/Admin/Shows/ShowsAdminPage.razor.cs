using FMFT.Extensions.Blazor.Bases.Loadings;
using FMFT.Web.Client.Models.API;
using FMFT.Web.Client.Models.API.Auditoriums;
using FMFT.Web.Client.Models.API.Shows;
using FMFT.Web.Shared.Enums;

namespace FMFT.Web.Client.Views.Pages.Admin.Shows
{
    public partial class ShowsAdminPage
    {
        public LoadingView LoadingView { get; set; }

        public List<Show> Shows => ShowsResponse.Object;
        public List<Auditorium> Auditoriums => AuditoriumsResponse.Object;

        public APIResponse<List<Show>> ShowsResponse { get; set; }
        public APIResponse<List<Auditorium>> AuditoriumsResponse { get; set; }

        protected override async Task OnInitializedAsync()
        {
            if (!UserAccountState.IsInRole(UserRole.Admin))
            {
                return;
            }

            ShowsResponse = await APIBroker.GetAllShowsAsync();
            AuditoriumsResponse = await APIBroker.GetAllAuditoriumsAsync();

            LoadingView.StopLoading();
        }

        public string GetAuditoriumName(Show show)
        {
            return Auditoriums.FirstOrDefault(x => x.Id == show.AuditoriumId)?.Name ?? string.Empty;
        }
            
        public int GetAuditoriumSeats(Show show)
        {
            return Auditoriums.FirstOrDefault(x => x.Id == show.AuditoriumId)?.Seats.Count ?? 0;
        }

        private string GetRowClass(Show show)
        {
            if (show.EndDateTime < DateTime.Now)
            {
                return "text-muted";
            }

            return string.Empty;
        }

        private string GetLinkClass(Show show)
        {
            if (show.EndDateTime < DateTime.Now)
            {
                return "link-secondary";
            }

            return string.Empty;
        }
    }
}
