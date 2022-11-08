using FMFT.Extensions.Blazor.Bases.Loadings;
using FMFT.Web.Client.Models.API;
using FMFT.Web.Client.Models.API.Auditoriums;
using FMFT.Web.Client.Models.API.Shows;
using Microsoft.AspNetCore.Components;

namespace FMFT.Web.Client.Views.Pages.Admin.Shows
{
    public partial class ShowAdminPage
    {
        [Parameter]
        public int ShowId { get; set; }

        public string ShowName => ShowResponse?.Object.Name ?? ShowId.ToString();

        public LoadingView LoadingView { get; set; }

        public APIResponse<Show> ShowResponse { get; set; }
        public APIResponse<List<Auditorium>> AuditoriumsResponse { get; set; }

        public Show Show => ShowResponse.Object;
        public List<Auditorium> Auditoriums => AuditoriumsResponse.Object;

        protected override async Task OnParametersSetAsync()
        {
            ShowResponse = await APIBroker.GetShowByIdAsync(ShowId);
            AuditoriumsResponse = await APIBroker.GetAllAuditoriumsAsync();

            LoadingView.StopLoading();
        }
    }
}
