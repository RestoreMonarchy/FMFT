using BlazorPanzoom;
using FMFT.Extensions.Blazor.Bases.Loadings;
using FMFT.Extensions.Blazor.Bases.Steppers;
using FMFT.Web.Client.Models.API;
using FMFT.Web.Client.Models.API.Auditoriums;
using FMFT.Web.Client.Models.API.Seats;
using FMFT.Web.Client.Models.API.Shows;
using Microsoft.AspNetCore.Components;

namespace FMFT.Web.Client.Views.Pages.Home.Shows
{
    public partial class ShowReservePage
    {
        [Parameter]
        public int ShowId { get; set; }

        private string ShowName => ShowResponse?.Object.Name ?? ShowId.ToString();

        private LoadingView LoadingView { get; set; }
        private Stepper Stepper { get; set; }

        public APIResponse<Show> ShowResponse { get; set; }
        public APIResponse<Auditorium> AuditoriumResponse { get; set; }

        public Show Show => ShowResponse.Object;
        public Auditorium Auditorium => AuditoriumResponse.Object;

        protected override async Task OnParametersSetAsync()
        {
            ShowResponse = await APIBroker.GetShowByIdAsync(ShowId);
            if (ShowResponse.IsSuccessfull)
            {
                AuditoriumResponse = await APIBroker.GetAuditoriumByIdAsync(Show.AuditoriumId);
            }
            LoadingView.StopLoading();
        }

        public Panzoom AuditoriumPanzoom { get; set; }

        public Seat SelectedSeat { get; set; }

        private Task NexToConfirmAsync()
        {
            Stepper.StepUp();
            return Task.CompletedTask;
        }

        private Task NextToSummaryAsync()
        {
            Stepper.StepUp();
            return Task.CompletedTask;
        }
    }
}
