using FMFT.Web.Client.Models.API;
using FMFT.Web.Client.Models.API.Shows;

namespace FMFT.Web.Client.Views.Pages.Home
{
    public partial class ShowsPage
    {
        public APIResponse<List<Show>> ShowsResponse { get; set; }

        protected override async Task OnInitializedAsync()
        {
            ShowsResponse = await APIBroker.GetAllShowsAsync();
        }
    }
}
