using FMFT.Extensions.Blazor.Bases.Buttons;
using FMFT.Extensions.Blazor.Bases.Loadings;
using FMFT.Extensions.Blazor.Bases.Steppers;
using FMFT.Web.Client.Models.API;
using FMFT.Web.Client.Models.API.Auditoriums;
using FMFT.Web.Client.Models.API.Reservations;
using FMFT.Web.Client.Models.API.ShowProducts;
using FMFT.Web.Client.Models.API.Shows;
using FMFT.Web.Client.Models.Services.Orders;
using Microsoft.AspNetCore.Components;

namespace FMFT.Web.Client.Views.Pages.Home.Shows
{
    public partial class ShowOrderPage
    {
        [Parameter]
        public int ShowId { get; set; }

        private string ShowName => ShowResponse?.Object?.Name ?? ShowId.ToString();

        private LoadingView LoadingView { get; set; }
        private Stepper Stepper { get; set; }
        public ButtonBase ConfirmButton { get; set; }
        public ButtonBase BackButton { get; set; }

        public APIResponse<Show> ShowResponse { get; set; }
        public APIResponse<Auditorium> AuditoriumResponse { get; set; }
        public APIResponse<List<Reservation>> UserReservationsResponse { get; set; }
        public APIResponse<Reservation> ReservationResponse { get; set; }
        public APIResponse<List<ShowProduct>> ShowProductsResponse { get; set; }

        public Show Show => ShowResponse.Object;
        public Auditorium Auditorium => AuditoriumResponse.Object;
        public List<Reservation> UserReservations => UserReservationsResponse.Object;
        public Reservation Reservation => ReservationResponse.Object;
        public List<ShowProduct> ShowProducts => ShowProductsResponse.Object;

        public Reservation UserReservation => UserReservations.OrderByDescending(x => x.CreateDate).FirstOrDefault(x => !x.IsCanceled);

        public OrderState OrderState { get; set; }

        protected override async Task OnParametersSetAsync()
        {
            if (!UserAccountState.IsAuthenticated)
            {
                return;
            }

            OrderState = new();

            Task[] getDataTasks = new Task[]
            {
                GetShowResponseAsync(),
                GetAuditoriumResponseAsync(),
                GetUserReservationsResponseAsync(),
                GetShowProductsResponseAsync()
            };

            await Task.WhenAll(getDataTasks);

            LoadingView.StopLoading();
        }

        private async Task GetShowResponseAsync()
        {
            ShowResponse = await APIBroker.GetShowByIdAsync(ShowId);
        }

        private async Task GetAuditoriumResponseAsync()
        {
            AuditoriumResponse = await APIBroker.GetAuditoriumByShowIdAsync(ShowId);
        }

        private async Task GetShowProductsResponseAsync()
        {
            ShowProductsResponse = await APIBroker.GetShowProductsByShowIdAsync(ShowId);
        }

        private async Task GetUserReservationsResponseAsync()
        {
            if (!UserAccountState.IsAuthenticated)
            {
                return;
            }

            UserReservationsResponse = await APIBroker.GetReservationsByUserAndShowIdAsync(UserAccountState.UserAccount.UserId, ShowId);
        }
    }
}
