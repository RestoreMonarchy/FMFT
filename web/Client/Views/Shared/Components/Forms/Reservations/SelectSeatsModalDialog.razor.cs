using FMFT.Extensions.Blazor.Bases.Buttons;
using FMFT.Extensions.Blazor.Bases.Dialogs;
using FMFT.Extensions.Blazor.Bases.Loadings;
using FMFT.Web.Client.Models.API.Auditoriums;
using FMFT.Web.Client.Models.API.Seats;
using FMFT.Web.Client.Models.API.Shows;
using FMFT.Web.Client.Views.Shared.Components.Panzooms;
using Microsoft.AspNetCore.Components;

namespace FMFT.Web.Client.Views.Shared.Components.Forms.Reservations
{
    public partial class SelectSeatsModalDialog
    {
        [Parameter]
        public EventCallback<List<Seat>> OnSeatsSelected { get; set; }

        public Auditorium Auditorium { get; set; }
        public List<ShowReservedItem> ReservedItems { get; set; }        

        public ModalDialog ModalDialog { get; set; }
        public LoadingView LoadingView { get; set; }
        public AuditoriumSeatPanzoom AuditoriumSeatPanzoom { get; set; }
        public ButtonBase ConfirmButton { get; set; }

        public int SeatsCount { get; set; }
        public List<Seat> Seats { get; set; } = new();
        private bool IsConfirmDisabled => SeatsCount != Seats.Count;

        protected override void OnAfterRender(bool firstRender)
        {
            if (firstRender)
            {
                LoadingView.Hide();
            }
        }

        public void SetAuditoriumAsync(Auditorium auditorium, List<ShowReservedItem> reservedItems)
        {
            Auditorium = auditorium;
            ReservedItems = reservedItems;
            Seats.Clear();
        }

        public async Task OpenAsync(int seatsCount)
        {
            LoadingView.StartLoading();

            SeatsCount = seatsCount;
            Seats.Clear();

            await ModalDialog.ShowStaticAsync();

            LoadingView.Show();
            LoadingView.StopLoading();
        }

        private async Task HandleConfirmAsync()
        {
            ConfirmButton.StartSpinning();
            await OnSeatsSelected.InvokeAsync(Seats);
            await ModalDialog.HideAsync();
            ConfirmButton.StopSpinning();
        }
    }
}
