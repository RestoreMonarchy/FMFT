using FMFT.Web.Client.Models.API;
using FMFT.Web.Client.Models.API.Auditoriums;
using FMFT.Web.Client.Models.API.Shows;
using Microsoft.AspNetCore.Components;

namespace FMFT.Web.Client.Views.Pages.Home
{
    public partial class ShowPage
    {
        [Parameter]
        public int ShowId { get; set; }

        private string ShowName => ShowResponse?.Object.Name ?? ShowId.ToString();

        public APIResponse<Show> ShowResponse { get; set; }
        public APIResponse<Auditorium> AuditoriumResponse { get; set; }

        public Show Show => ShowResponse.Object;

        protected override async Task OnParametersSetAsync()
        {
            ShowResponse = await APIBroker.GetShowByIdAsync(ShowId);
        }
    }
}
