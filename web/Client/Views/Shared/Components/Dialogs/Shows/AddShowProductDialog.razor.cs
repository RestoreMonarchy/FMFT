using FMFT.Extensions.Blazor.Bases.Alerts;
using FMFT.Extensions.Blazor.Bases.Buttons;
using FMFT.Extensions.Blazor.Bases.Dialogs;
using FMFT.Web.Client.Models.API;
using FMFT.Web.Client.Models.API.ShowProducts;
using FMFT.Web.Client.Models.API.ShowProducts.Requests;
using FMFT.Web.Client.Models.API.Shows;
using FMFT.Web.Client.Models.Forms.ShowProducts;
using Microsoft.AspNetCore.Components;

namespace FMFT.Web.Client.Views.Shared.Components.Dialogs.Shows
{
    public partial class AddShowProductDialog
    {
        [Parameter]
        public Show Show { get; set; }

        [Parameter]
        public EventCallback<ShowProduct> OnShowProductAdded { get; set; }

        public ModalDialog ModalDialog { get; set; }
        public SubmitButtonBase SubmitButton { get; set; }
        public AlertGroupBase AlertGroup { get; set; }
        public AlertBase ErrorAlert { get; set; }
        public AlertBase SuccessAlert { get; set; }

        public AddShowProductFormModel Model { get; set; } = new()
        {
            Name = null,
            Price = 0,
            IsEnabled = true
        };

        private void ResetModel()
        {
            Model = new()
            {
                Name = null,
                Price = 0,
                IsEnabled = true
            };
        }

        public async Task ShowAsync()
        {
            AlertGroup.HideAll();
            ResetModel();
            await ModalDialog.ShowStaticAsync();
        }

        private async Task HideAsync()
        {
            await ModalDialog.HideAsync();
        }

        private async Task SubmitAsync()
        {
            AlertGroup.HideAll();

            AddShowProductRequest request = new()
            {
                ShowId = Show.Id,
                Name = Model.Name,
                Price = Model.Price.Value,
                IsEnabled = Model.IsEnabled
            };

            APIResponse<ShowProduct> response = await APIBroker.AddShowProductAsync(request);

            if (response.IsSuccessful)
            {
                await OnShowProductAdded.InvokeAsync(response.Object);
                SuccessAlert.Show();
                ResetModel();
                await ModalDialog.HideAsync();
            }
            else
            {
                ErrorAlert.Show();
            }
        }
    }
}
