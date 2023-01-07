﻿using FMFT.Extensions.Blazor.Bases.Alerts;
using FMFT.Extensions.Blazor.Bases.Buttons;
using FMFT.Extensions.Blazor.Bases.Dialogs;
using FMFT.Web.Client.Models.API;
using FMFT.Web.Client.Models.API.ShowProducts;
using FMFT.Web.Client.Models.API.ShowProducts.Requests;
using FMFT.Web.Client.Models.Forms.ShowProducts;
using Microsoft.AspNetCore.Components;

namespace FMFT.Web.Client.Views.Shared.Components.Dialogs.Shows
{
    public partial class EditShowProductDialog
    {
        [Parameter]
        public EventCallback<ShowProduct> OnShowProductUpdated { get; set; }

        public ModalDialog ModalDialog { get; set; }
        public AlertGroupBase AlertGroup { get; set; }
        public AlertBase SuccessAlert { get; set; }
        public AlertBase ErrorAlert { get; set; }
        public SubmitButtonBase SubmitButton { get; set; }
        
        public ModifyShowProductFormModel Model { get; set; }
        public ShowProduct ShowProduct { get; set; }

        public async Task ShowAsync(ShowProduct showProduct)
        {
            await ModalDialog.ShowAsync();
            ShowProduct = showProduct;
            Model = new()
            {
                Name = showProduct.Name,
                Price = showProduct.Price,
                IsEnabled = showProduct.IsEnabled
            };
            StateHasChanged();
        }

        private async Task HideAsync()
        {
            await ModalDialog.HideAsync();
        }

        private async Task SubmitAsync()
        {
            AlertGroup.HideAll();

            ModifyShowProductRequest request = new()
            {
                Id = ShowProduct.Id,
                ShowId = ShowProduct.Id,
                Name = Model.Name,
                Price = Model.Price.Value,
                IsEnabled = Model.IsEnabled
            };

            APIResponse<ShowProduct> response = await APIBroker.ModifyShowProductAsync(request);

            if (response.IsSuccessful)
            {
                await OnShowProductUpdated.InvokeAsync(response.Object);
                SuccessAlert.Show();
            } else
            {
                ErrorAlert.Show();
            }
        }
    }
}
