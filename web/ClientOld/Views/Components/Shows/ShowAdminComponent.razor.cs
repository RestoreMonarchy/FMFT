using FMFT.Web.Client.Models.Auditoriums;
using FMFT.Web.Client.Models.Auditoriums.Exceptions;
using FMFT.Web.Client.Models.Shows;
using FMFT.Web.Client.Models.Shows.Exceptions;
using FMFT.Web.Client.Services.Views.Shows;
using Microsoft.AspNetCore.Components;

namespace FMFT.Web.Client.Views.Components.Shows
{
    public partial class ShowAdminComponent
    {
        [Parameter]
        public int ShowId { get; set; }

        [Inject]
        public IShowViewService ShowViewService { get; set; }

        public Show Show { get; set; }
        public List<Auditorium> Auditoriums { get; set; }

        public Exception Exception { get; set; }

        protected override async Task OnParametersSetAsync()
        {
            try
            {
                Show = await ShowViewService.RetrieveShowByIdAsync(ShowId);
                Auditoriums = await ShowViewService.RetrieveAllAuditoriumsAsync();
            } catch (ShowNotFoundException e)
            {
                Exception = e;
            }          
        }
    }
}
