using FMFT.Extensions.Blazor.Bases.Loadings;
using FMFT.Web.Client.Models.API;
using FMFT.Web.Client.Models.API.Auditoriums;

namespace FMFT.Web.Client.Views.Pages.Admin.Shows
{
    public partial class AddShowAdminPage
    {
        public LoadingView LoadingView { get; set; }

        public APIResponse<List<Auditorium>> AuditoriumsResponse { get; set; }

        public List<Auditorium> Auditoriums => AuditoriumsResponse.Object;

        protected override async Task OnInitializedAsync()
        {
            AuditoriumsResponse = await APIBroker.GetAllAuditoriumsAsync();
            LoadingView.StopLoading();
        }
    }
}
