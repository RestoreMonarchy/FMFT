using FMFT.Extensions.Blazor.Bases.Buttons;
using FMFT.Web.Client.Models.API.Auditoriums;
using FMFT.Web.Client.Models.API.Reservations.Requests;
using FMFT.Web.Client.Models.API.Seats;
using FMFT.Web.Client.Models.API.Shows;
using FMFT.Web.Client.Models.Forms.Reservations;
using FMFT.Web.Client.Views.Shared.Components.Panzooms;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Headers;

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
        public AuditoriumSeatPanzoom AuditoriumSeatPanzoom { get; set; }

        public Show Show => Shows.First(x => x.Id == Model.ShowId);
        public Auditorium Auditorium => Auditoriums.First(x => x.Id == Show.AuditoriumId);

        private async Task HandleShowIdChangeAsync(ChangeEventArgs args)
        {
            int? value = null;
            if (int.TryParse(args.Value.ToString(), out int num))
            {
                value = num;
            }

            Model.ShowId = value;
            Model.Seats = new();
            
            if (AuditoriumSeatPanzoom != null)
            {
                await AuditoriumSeatPanzoom.ReloadAsync();
            }
        }

        private async Task SubmitAsync()
        {
            Console.WriteLine("hello");
        }
    }
}
