using FMFT.Extensions.Blazor.Bases.Buttons;
using FMFT.Extensions.Blazor.Bases.Loadings;
using FMFT.Web.Client.Models.API.Auditoriums;
using FMFT.Web.Client.Models.API.Reservations.Requests;
using FMFT.Web.Client.Models.API.Seats;
using FMFT.Web.Client.Models.API.Shows;
using FMFT.Web.Client.Models.Forms.Reservations;
using FMFT.Web.Client.Views.Shared.Components.Panzooms;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Headers;
using System.Threading;

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
                Console.WriteLine("release");
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

            Console.WriteLine("Start Loading");

            semaphoreSlim = new SemaphoreSlim(0);
            await semaphoreSlim.WaitAsync();

            Console.WriteLine("Stop Loading");

            SeatSelectorLoadingView.StopLoading();      
        }

        private async Task SubmitAsync()
        {
            Console.WriteLine("hello");
        }
    }
}
