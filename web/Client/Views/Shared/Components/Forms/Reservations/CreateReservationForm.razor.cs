using FMFT.Extensions.Blazor.Bases.Alerts;
using FMFT.Extensions.Blazor.Bases.Buttons;
using FMFT.Extensions.Blazor.Bases.Dialogs;
using FMFT.Extensions.Blazor.Bases.Loadings;
using FMFT.Web.Client.Models.API;
using FMFT.Web.Client.Models.API.Auditoriums;
using FMFT.Web.Client.Models.API.Reservations;
using FMFT.Web.Client.Models.API.Reservations.Requests;
using FMFT.Web.Client.Models.API.Seats;
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
        public LoadingView ProductsLoadingView { get; set; }
        public SelectSeatsModalDialog SelectSeatsModalDialog { get; set; }

        public AlertGroupBase AlertGroup { get; set; }
        public AlertBase SeatAlertReservedAlert { get; set; }
        public AlertBase UserAlreadyReservedAlert { get; set; }
        public AlertBase SeatsNotProvidedAlert { get; set; }
        public AlertBase ValidationErrorAlert { get; set; }
        public AlertBase DuplicateSeatErrorAlert { get; set; }
        public AlertBase ErrorAlert { get; set; }
        public AlertBase SuccessAlert { get; set; }

        public Show Show => Shows.First(x => x.Id == Model.ShowId);
        public Auditorium Auditorium => Auditoriums.First(x => x.Id == Show.AuditoriumId);

        public APIResponse<List<ShowProduct>> ShowProductsResponse { get; set; }
        public List<ShowProduct> ShowProducts => ShowProductsResponse.Object;

        protected override void OnAfterRender(bool firstRender)
        {
            if (firstRender)
            {
                ProductsLoadingView.Hide();
            }            
        }

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
                SelectSeatsModalDialog.SetAuditoriumAsync(Auditorium, Show.ReservedItems);

                ProductsLoadingView.Show();
                ProductsLoadingView.StartLoading();
                ShowProductsResponse = await APIBroker.GetShowProductsByShowIdAsync(value.Value);
                ProductsLoadingView.StopLoading();
            }
            else
            {
                ProductsLoadingView.Hide();
            }     
        }

        private void HandleRemoveModelItem(CreateReservationFormModel.Item item)
        {
            Model.Items.Remove(item);
        }

        private async Task HandleShowProductAddedAsync(Tuple<ShowProduct, int> tuple)
        {
            if (tuple.Item1.IsBulk)
            {
                for (int i = 0; i < tuple.Item2; i++)
                {
                    CreateReservationFormModel.Item item = new()
                    {
                        ShowProduct = tuple.Item1,
                        Seat = null
                    };
                    Model.Items.Add(item);
                }
            } else
            {
                selectedSeatsProduct = tuple.Item1;
                await SelectSeatsModalDialog.OpenAsync(tuple.Item2);
            }
        }

        private ShowProduct selectedSeatsProduct;

        private async Task HandleOnSeatsSelectedAsync(List<Seat> seats)
        {
            if (selectedSeatsProduct == null)
            {
                return;
            }

            foreach (Seat seat in seats)
            {
                Model.Items.Add(new() 
                { 
                    ShowProduct = selectedSeatsProduct,
                    Seat = seat
                });
            }
        }

        private async Task HandleSubmitAsync()
        {
            AlertGroup.HideAll();
            SubmitButton.StartSpinning();

            CreateReservationRequest request = new()
            {
                ShowId = Model.ShowId.Value,
                UserId = Model.UserId,
                Email = Model.Email,
                FirstName = Model.FirstName,
                LastName = Model.LastName,
                Items = Model.Items.Select(x => new CreateReservationRequest.Item() 
                { 
                    SeatId = x.Seat?.Id ?? null,
                    ShowProductId = x.ShowProduct.Id
                }).ToList()
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
                    case "ERR058":
                        DuplicateSeatErrorAlert.Show();
                        break;
                    default:
                        ErrorAlert.Show();
                        break;
                }
            }

            SubmitButton.StopSpinning();
        }
    }
}
