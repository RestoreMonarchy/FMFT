using FMFT.Extensions.Blazor.Bases.Buttons;
using FMFT.Extensions.Blazor.Bases.Dialogs;
using FMFT.Extensions.Blazor.Bases.Loadings;
using FMFT.Web.Client.Models.API;
using FMFT.Web.Client.Models.API.Auditoriums;
using FMFT.Web.Client.Models.API.Reservations;
using FMFT.Web.Client.Models.API.Reservations.Requests;
using FMFT.Web.Client.Models.API.Shows;
using FMFT.Web.Client.Models.Forms.Reservations;
using FMFT.Web.Client.Views.Shared.Components.Panzooms;
using Microsoft.AspNetCore.Components;

namespace FMFT.Web.Client.Views.Shared.Components.Forms.Reservations
{
    public partial class CreateReservationForm
    {
        [Parameter]
        public List<Show> Shows { get; set; }
        [Parameter]
        public List<Auditorium> Auditoriums { get; set; }

        public CreateReservationFormModel Model { get; set; } = new()
        {
            Seats = new()
        };

        public SubmitButtonBase SubmitButton { get; set; }
        public LoadingView SeatSelectorLoadingView { get; set; }
        public AuditoriumSeatPanzoom AuditoriumSeatPanzoom { get; set; }
        public ModalDialog SubmitModalDialog { get; set; }
        public ButtonBase SubmitConfirmButton { get; set; }

        public Show Show => Shows.First(x => x.Id == Model.ShowId);
        public Auditorium Auditorium => Auditoriums.First(x => x.Id == Show.AuditoriumId);

        protected override void OnAfterRender(bool firstRender)
        {
            if (firstRender)
            {
                SeatSelectorLoadingView.Hide();
            }

            if (semaphoreSlim != null)
            {
                semaphoreSlim.Release();
                semaphoreSlim = null;
            }
        }

        private SemaphoreSlim semaphoreSlim = new(0);

        private async Task HandleShowIdChangeAsync(ChangeEventArgs args)
        {
            int? value = null;
            if (int.TryParse(args.Value.ToString(), out int num))
            {
                value = num;
            }

            Model.ShowId = value;
            Model.Seats = new();

            if (value == null)
            {
                SeatSelectorLoadingView.Hide();
            } else
            {
                SeatSelectorLoadingView.Show();
            }

            SeatSelectorLoadingView.StartLoading();

            semaphoreSlim = new SemaphoreSlim(0);
            await semaphoreSlim.WaitAsync();

            SeatSelectorLoadingView.StopLoading();      
        }

        private async Task HandleSubmitAsync()
        {
            StateHasChanged();
            await SubmitModalDialog.ShowAsync();
        }

        private async Task HandleConfirmSubmitAsync()
        {
            SubmitConfirmButton.StartSpinning();

            CreateReservationRequest request = new()
            {
                ShowId = Model.ShowId.Value,
                UserId = Model.UserId,
                SeatIds = Model.Seats.Select(x => x.Id).ToList(),
                Email = Model.Email,
                FirstName = Model.FirstName,
                LastName = Model.LastName
            };

            APIResponse<Reservation> response = await APIBroker.CreateReservationAsync(request);

            if (response.IsSuccessful)
            {
                NavigationBroker.NavigateTo($"/admin/reservations/{response.Object.Id}");
            }

        }
    }
}
