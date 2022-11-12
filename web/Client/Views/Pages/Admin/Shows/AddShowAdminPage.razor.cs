using FMFT.Extensions.Blazor.Bases.Loadings;
using FMFT.Web.Client.Models.API;
using FMFT.Web.Client.Models.API.Auditoriums;
using FMFT.Web.Shared.Enums;

namespace FMFT.Web.Client.Views.Pages.Admin.Shows
{
    public partial class AddShowAdminPage
    {
        public LoadingView LoadingView { get; set; }

        public APIResponse<List<Auditorium>> AuditoriumsResponse { get; set; }

        public List<Auditorium> Auditoriums => AuditoriumsResponse.Object;

        protected override async Task OnInitializedAsync()
        {
            if (!UserAccountState.IsInRole(UserRole.Admin))
            {
                return;
            }

            AuditoriumsResponse = await APIBroker.GetAllAuditoriumsAsync();
            LoadingView.StopLoading();
        }
    }
}
