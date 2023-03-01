using FMFT.Extensions.Blazor.Bases.Alerts;
using FMFT.Extensions.Blazor.Bases.Loadings;
using Microsoft.AspNetCore.Components;

namespace FMFT.Web.Client.Views.Pages.Staff
{
    public partial class ReservationsStaffPage
    {
        [Parameter]
        public string ReservationId { get; set; }

        public AlertGroupBase AlertGroup { get; set; }
        public AlertBase NotFoundAlert { get; set; }
        public AlertBase SuccessAlert { get; set; }
        public LoadingView LoadingView { get; set; }

        protected override async Task OnParametersSetAsync()
        {

        }
    }
}
