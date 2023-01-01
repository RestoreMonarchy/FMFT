using FMFT.Web.Client.Models.API.Auditoriums;
using FMFT.Web.Client.Models.API.Shows;
using Microsoft.AspNetCore.Components;

namespace FMFT.Web.Client.Views.Pages.Admin.Shows.Subpages
{
    public partial class EditShowAdminSubpage
    {
        [Parameter]
        public Show Show { get; set; }
        [Parameter]
        public EventCallback<Show> ShowChanged { get; set; }

        [Parameter]
        public List<Auditorium> Auditoriums { get; set; }

        private async Task HandleShowChangedAsync(Show show)
        {
            Show = show;
            await ShowChanged.InvokeAsync(show);
        }
    }
}
