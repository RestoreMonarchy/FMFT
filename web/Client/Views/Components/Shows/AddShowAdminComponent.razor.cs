using FMFT.Web.Client.Models.Auditoriums;
using FMFT.Web.Client.Services.Views.Shows;
using Microsoft.AspNetCore.Components;

namespace FMFT.Web.Client.Views.Components.Shows
{
    public partial class AddShowAdminComponent
    {
        [Inject]
        public IShowViewService ShowViewService { get; set; }

        public List<Auditorium> Auditoriums { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Auditoriums = await ShowViewService.RetrieveAllAuditoriumsAsync();
        }
    }
}
