using FMFT.Web.Client.Services.Foundations.Shows;
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
        public IShowService ShowService { get; set; }

        public Show Show { get; set; }

        public Exception Exception { get; set; }

        protected override async Task OnInitializedAsync()
        {
            try
            {
                Show = await ShowService.RetrieveShowByIdAsync(ShowId);
            } catch (ShowNotFoundException e)
            {
                Exception = e;
            }            
        }
    }
}
