using FMFT.Extensions.Blazor.Bases.Buttons;
using FMFT.Extensions.Blazor.Bases.Loadings;
using FMFT.Extensions.Blazor.Bases.Navigations;
using FMFT.Extensions.Blazor.Bases.Steppers;
using FMFT.Web.Client.Brokers.Storages;
using FMFT.Web.Client.Models.API;
using FMFT.Web.Client.Models.API.Auditoriums;
using FMFT.Web.Client.Models.API.Reservations;
using FMFT.Web.Client.Models.API.Seats;
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
        [Parameter]
        public string ActiveNavigationKey { get; set; } = "tickets";

        private string[] steps = new string[]
        {
            "tickets",
            "seats",
            "payment"
        };

        private string ShowName => ShowResponse?.Object?.Name ?? ShowId.ToString();

        private LoadingView LoadingView { get; set; }
        private NavigationStepper Stepper { get; set; }
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

        protected override async Task OnInitializedAsync()
        {
            if (!UserAccountState.IsAuthenticated)
            {
                return;
            }

            Task[] getDataTasks = new Task[]
            {
                GetShowResponseAsync(),
                GetAuditoriumResponseAsync(),
                GetShowProductsResponseAsync(),
                GetUserReservationsResponseAsync()
            };

            await Task.WhenAll(getDataTasks);

            await RetrieveOrderStateAsync();

            LoadingView.StopLoading();

            CheckActiveStep();
        }

        private void CheckActiveStep()
        {
            if (steps.Any(x => x.Equals(ActiveNavigationKey, StringComparison.OrdinalIgnoreCase)))
            {
                return;
            }

            if (string.IsNullOrEmpty(OrderState.CurrentStepKey))
            {
                return;
            }

            if (OrderState.CurrentStepKey.Equals(ActiveNavigationKey, StringComparison.OrdinalIgnoreCase))
            {
                return;
            }

            ActiveNavigationKey = OrderState.CurrentStepKey;
            NavigateToStepKey(OrderState.CurrentStepKey);
        }
            
        private async Task RetrieveOrderStateAsync()
        {
            OrderStateData orderStateData = await StorageBroker.GetOrderStateDataAsync(ShowId);

            OrderState = new();
            if (orderStateData != null)
            {
                OrderState.CurrentStepKey = orderStateData.CurrentStepKey;
                OrderState.PaymentMethod = orderStateData.PaymentMethod;

                foreach (OrderStateItemData item in orderStateData.Items)
                {
                    if (item.ShowId != ShowId)
                    {
                        continue;
                    }

                    ShowProduct showProduct = ShowProducts.FirstOrDefault(x => x.Id == item.ShowProductId);

                    if (showProduct == null)
                    {
                        continue;
                    }

                    OrderState.Items.Add(new OrderItemState()
                    {
                        Show = Show,
                        ShowProduct = showProduct,
                        Quantity = item.Quantity
                    });
                }

                foreach (int seatId in orderStateData.SeatIds)
                {
                    Seat seat = Auditorium.Seats.FirstOrDefault(x => x.Id == seatId);

                    if (seat == null)
                    {
                        continue;
                    }

                    if (Show.ReservedSeats.Exists(x => x.SeatId == seat.Id))
                    {
                        continue;
                    }

                    OrderState.Seats.Add(seat);
                }

                if (OrderState.Items.Count != orderStateData.Items.Count
                    || OrderState.Seats.Count != orderStateData.SeatIds.Count)
                {
                    await SaveOrderStateAsync(OrderState);
                }
            } else
            {
                OrderState = new();
                await SaveOrderStateAsync(OrderState);
            }
        }

        private async Task SaveOrderStateAsync(OrderState orderState)
        {
            OrderState.CurrentStepKey = ActiveNavigationKey;
            await StorageBroker.SetOrderStateDataAsync(ShowId, orderState.ToOrderStateData());
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

        private async Task HandleNavigate(NavigationItem navigationItem)
        {
            NavigateToStepKey(navigationItem.Key);

            OrderState.CurrentStepKey = navigationItem.Key;
            await SaveOrderStateAsync(OrderState);
        }

        private void NavigateToStepKey(string stepKey)
        {            
            NavigationBroker.NavigateTo($"/shows/{ShowId}/order/{stepKey}");            
        }
    }
}
