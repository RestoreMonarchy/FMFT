using FMFT.Web.Client.Services.Views.Shows;
using FMFT.Web.Shared.Models.Shows;
using Microsoft.AspNetCore.Components;

namespace FMFT.Web.Client.Views.Components.Shows
{
    public partial class ShowsComponent
    {
        [Inject]
        public IShowViewService ShowViewService { get; set; }

        public List<Show> Shows { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Shows = await ShowViewService.RetrieveAllShowsAsync();
        }
    }
}
