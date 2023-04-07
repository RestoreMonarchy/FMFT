using FMFT.Extensions.Blazor.Bases.Alerts;
using FMFT.Extensions.Blazor.Bases.Buttons;
using FMFT.Extensions.Blazor.Bases.Dialogs;
using FMFT.Extensions.Blazor.Bases.Loadings;
using FMFT.Web.Client.Models.API;
using FMFT.Web.Client.Models.API.Auditoriums;
using FMFT.Web.Client.Models.API.Reservations;
using FMFT.Web.Client.Models.API.Reservations.Requests;
using FMFT.Web.Client.Models.API.ShowProducts;
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
            Items = new()
        };

        public SubmitButtonBase SubmitButton { get; set; }
        public LoadingView SeatSelectorLoadingView { get; set; }
        public AuditoriumSeatPanzoom AuditoriumSeatPanzoom { get; set; }
        public ModalDialog SubmitModalDialog { get; set; }
        public ButtonBase SubmitConfirmButton { get; set; }
        public LoadingView ProductsLoadingView { get; set; }

        public AlertGroupBase AlertGroup { get; set; }
        public AlertBase SeatAlertReservedAlert { get; set; }
        public AlertBase UserAlreadyReservedAlert { get; set; }
        public AlertBase SeatsNotProvidedAlert { get; set; }
        public AlertBase ValidationErrorAlert { get; set; }
        public AlertBase SuccessAlert { get; set; }

        public Show Show => Shows.First(x => x.Id == Model.ShowId);
        public Auditorium Auditorium => Auditoriums.First(x => x.Id == Show.AuditoriumId);

        public APIResponse<List<ShowProduct>> ShowProductsResponse { get; set; }
        public List<ShowProduct> ShowProducts => ShowProductsResponse.Object;



        protected override void OnAfterRender(bool firstRender)
        {
            if (firstRender)
            {
                SeatSelectorLoadingView.Hide();
                ProductsLoadingView.Hide();
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
            Model.Items = new();

            if (value.HasValue)
            {
                ProductsLoadingView.Show();
                ProductsLoadingView.StartLoading();
                ShowProductsResponse = await APIBroker.GetShowProductsByShowIdAsync(value.Value);
                ProductsLoadingView.StopLoading();
            } else
            {
                ProductsLoadingView.Hide();
            }            


            //if (value.HasValue)
            //{
            //    ShowProductsResponse = await APIBroker.GetShowProductsByShowIdAsync(value.Value);
            //    SeatSelectorLoadingView.Show();                
            //} else
            //{
            //    SeatSelectorLoadingView.Hide();
            //}

            //SeatSelectorLoadingView.StartLoading();

            //semaphoreSlim = new SemaphoreSlim(0);
            //await semaphoreSlim.WaitAsync();

            //SeatSelectorLoadingView.StopLoading();      
        }

        private async Task HandleShowProductIdChangeAsync(ChangeEventArgs args)
        {

        }

        private async Task HandleSubmitAsync()
        {
            StateHasChanged();
            await SubmitModalDialog.ShowAsync();
            AlertGroup.HideAll();
        }

        private async Task HandleConfirmSubmitAsync()
        {
            AlertGroup.HideAll();
            SubmitConfirmButton.StartSpinning();

            CreateReservationRequest request = new()
            {
                ShowId = Model.ShowId.Value,
                UserId = Model.UserId,
                Email = Model.Email,
                FirstName = Model.FirstName,
                LastName = Model.LastName
            };

            APIResponse<Reservation> response = await APIBroker.CreateReservationAsync(request);

            if (response.IsSuccessful)
            {
                NavigationBroker.NavigateTo($"/admin/reservations/{response.Object.Id}");
                SuccessAlert.Show();
                return;
            } else
            {
                switch (response.Error.Code)
                {
                    case "ERR035":
                        ValidationErrorAlert.Show();
                        break;
                    case "ERR018":
                        SeatAlertReservedAlert.Show();
                        break;
                    case "ERR033":
                        SeatsNotProvidedAlert.Show();
                        break;
                    case "ERR019":
                        UserAlreadyReservedAlert.Show();
                        break;
                }
            }

            SubmitConfirmButton.StopSpinning();
        }
    }
}
