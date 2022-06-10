using FMFT.Web.Client.Services.Foundations.Shows;
using FMFT.Web.Client.Services.Views.Shows;
using FMFT.Web.Shared.Models.Auditoriums;
using FMFT.Web.Shared.Models.Auditoriums.Exceptions;
using FMFT.Web.Shared.Models.Shows;
using FMFT.Web.Shared.Models.Shows.Exceptions;
using Microsoft.AspNetCore.Components;

namespace FMFT.Web.Client.Views.Components.Shows
{
    public partial class ShowComponent
    {
        [Parameter]
        public int ShowId { get; set; }

        [Inject]
        public IShowViewService ShowViewService { get; set; }

        public Show Show { get; set; }
        public Auditorium Auditorium { get; set; }

        public Exception Exception { get; set; }

        protected override async Task OnInitializedAsync()
        {
            try
            {
                Show = await ShowViewService.RetrieveShowByIdAsync(ShowId);
                Auditorium = await ShowViewService.RetrieveAuditoriumAsync(Show.AuditoriumId);
            } catch (ShowNotFoundException e)
            {
                Exception = e;
            } catch (AuditoriumNotFoundException e)
            {
                Exception = e;
            }
        }
    }
}
