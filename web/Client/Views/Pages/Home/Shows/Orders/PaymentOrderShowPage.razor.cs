﻿using FMFT.Extensions.Blazor.Bases.Alerts;
using FMFT.Extensions.Blazor.Bases.Buttons;
using FMFT.Extensions.Blazor.Bases.Loadings;
using FMFT.Web.Client.Models.API;
using FMFT.Web.Client.Models.API.Auditoriums;
using FMFT.Web.Client.Models.API.Orders;
using FMFT.Web.Client.Models.API.Orders.Requests;
using FMFT.Web.Client.Models.API.Seats;
using FMFT.Web.Client.Models.API.ShowProducts;
using FMFT.Web.Client.Models.API.Shows;
using FMFT.Web.Client.Models.Services.Orders;
using FMFT.Web.Client.Services.Pages;
using FMFT.Web.Shared.Enums;
using Microsoft.AspNetCore.Components;

namespace FMFT.Web.Client.Views.Pages.Home.Shows.Orders
{
    public partial class PaymentOrderShowPage
    {
        private const string PageKey = "payment";

        [Parameter]
        public int ShowId { get; set; }

        private string GetUrl(string subPage)
        {
            return $"/shows/{ShowId}/order/{subPage}";
        }

        [Inject]
        public OrderingPageService OrderingPageService { get; set; }

        public LoadingView LoadingView { get; set; }
        public AlertGroupBase AlertGroup { get; set; }
        public AlertBase ErrorAlert { get; set; }
        public AlertBase SeatErrorAlert { get; set; }
        public ButtonBase PayButton { get; set; }

        public OrderStateData OrderStateData { get; set; }
        public APIResponse<Show> ShowResponse { get; set; }
        public APIResponse<List<ShowProduct>> ShowProductsResponse { get; set; }
        public APIResponse<Auditorium> AuditoriumResponse { get; set; }

        public Show Show => ShowResponse.Object;
        public List<ShowProduct> ShowProducts => ShowProductsResponse.Object;
        public Auditorium Auditorium => AuditoriumResponse.Object;

        public decimal TotalPrice { get; private set; }
        public bool PayDisabled => !OrderStateData.IsAgreeTerms || !OrderStateData.IsAgreePrzelewy24;

        public IEnumerable<Seat> Seats => Auditorium.Seats.Where(x => OrderStateData.Items.Any(y => y.SeatIds.Contains(x.Id)));

        public IEnumerable<OrderItemStateData> BulkItems => OrderStateData.Items.Where(x => GetShowProduct(x.ShowProductId).IsBulk);
        public int BulkItemsCount => BulkItems.Sum(x => x.Quantity);

        protected override async Task OnInitializedAsync()
        {
            if (!UserAccountState.IsAuthenticated || !UserAccountState.IsEmailConfirmed)
            {
                return;
            }

            OrderStateData = await OrderingPageService.RetrieveOrderStateDataAsync(ShowId);

            if (!OrderStateData.IsValid())
            {
                NavigationBroker.NavigateTo(GetUrl("products"));
                return;
            }

            Task[] getDataTasks = new Task[]
            {
                GetShowResponseAsync(),
                GetShowProductsResponseAsync(),
                GetAudituriumResponseAsync()
            };

            await Task.WhenAll(getDataTasks);

            if (ShowResponse.IsSuccessful && ShowProductsResponse.IsSuccessful)
            {
                TotalPrice = GetTotalPrice();

                if (Show.IsPast() || Show.IsSellDisabled())
                {
                    NavigationBroker.NavigateTo($"/shows/{ShowId}");
                    return;
                }
            }

            LoadingView.StopLoading();
        }

        private async Task GetShowResponseAsync()
        {
            if (UserAccountState.IsInRole(UserRole.Admin))
            {
                ShowResponse = await APIBroker.GetShowByIdAsync(ShowId);
            }
            else
            {
                ShowResponse = await APIBroker.GetPublicShowByIdAsync(ShowId);
            }
        }

        private async Task GetShowProductsResponseAsync()
        {
            ShowProductsResponse = await APIBroker.GetShowProductsByShowIdAsync(ShowId);
        }

        private async Task GetAudituriumResponseAsync()
        {
            AuditoriumResponse = await APIBroker.GetAuditoriumByShowIdAsync(ShowId);
        }

        private ShowProduct GetShowProduct(int showProductId)
        {
            ShowProduct showProduct = ShowProducts.FirstOrDefault(x => x.Id == showProductId);

            if (showProduct == null)
            {
                throw new Exception("One of your products does not exist anymore. Clear your browser's local storage and create order again");
            }

            return showProduct;
        }

        private decimal GetTotalPrice()
        {
            decimal totalPrice = 0;
            foreach (OrderItemStateData orderItem in OrderStateData.Items)
            {
                ShowProduct showProduct = ShowProducts.FirstOrDefault(x => x.Id == orderItem.ShowProductId);
                decimal price = showProduct?.Price ?? 0;

                totalPrice += price * orderItem.Quantity;
            }

            return totalPrice;
        }

        private Task HandlePaymentMethodChangedAsync(PaymentMethod paymentMethod)
        {
            OrderStateData.PaymentMethod = paymentMethod;
            InvokeAsync(() => UpdateOrderStateDataAsync());

            return Task.CompletedTask;
        }

        private async Task HandlePayAsync()
        {
            AlertGroup.HideAll();
            PayButton.StartSpinning();

            CreateOrderRequest createOrderRequest = new()
            {
                Amount = TotalPrice,
                UserId = UserAccountState.UserAccount.UserId,
                Currency = "PLN",
                PaymentMethod = OrderStateData.PaymentMethod,
                Items = new()
            };

            foreach (OrderItemStateData orderItem in OrderStateData.Items)
            {
                if (orderItem.Quantity == 0)
                {
                    continue;
                }

                ShowProduct showProduct = GetShowProduct(orderItem.ShowProductId);

                createOrderRequest.Items.Add(new CreateOrderRequest.Item()
                {
                    ShowProductId = showProduct.Id,
                    Price = showProduct.Price,
                    Quantity = orderItem.Quantity,
                    SeatIds = orderItem.SeatIds
                });
            }

            APIResponse<Order> response = await APIBroker.CreateOrderAsync(createOrderRequest);
            if (response.IsSuccessful)
            {
                await OrderingPageService.ResetOrderStateDataAsync(ShowId);

                Order order = response.Object;
                APIResponse<PaymentUrl> paymentResponse = await APIBroker.GetOrderPaymentUrlAsync(order.Id);
                NavigationBroker.NavigateTo(paymentResponse.Object.Url);
            }
            else
            {
                if (response.Error.Code == "ERR018")
                {
                    SeatErrorAlert.Show();
                } else
                {
                    ErrorAlert.Show();
                }

                await AlertGroup.FocusAsync();
                
                PayButton.StopSpinning();
            }
        }

        private async Task UpdateOrderStateDataAsync()
        {
            OrderStateData.CurrentStepKey = PageKey;
            await OrderingPageService.SaveOrderStateDataAsync(OrderStateData);
        }
    }
}
