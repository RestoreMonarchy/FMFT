using FMFT.Extensions.Blazor.Bases.Loadings;
using FMFT.Web.Client.Models.API;
using FMFT.Web.Client.Models.API.ShowProducts;
using FMFT.Web.Client.Models.API.Shows;
using FMFT.Web.Client.Views.Shared.Components.Dialogs.Shows;
using Microsoft.AspNetCore.Components;

namespace FMFT.Web.Client.Views.Pages.Admin.Shows.Subpages
{
    public partial class ProductsShowAdminSubpage
    {
        [Parameter]
        public Show Show { get; set; }

        public LoadingView LoadingView { get; set; }
        public AddShowProductDialog AddShowProductDialog { get; set; }
        public EditShowProductDialog EditShowProductDialog { get; set; }

        public APIResponse<List<ShowProduct>> ShowProductsResponse { get; set; }

        public List<ShowProduct> ShowProducts => ShowProductsResponse.Object;

        protected override async Task OnParametersSetAsync()
        {
            ShowProductsResponse = await APIBroker.GetShowProductsByShowIdAsync(Show.Id);

            LoadingView.StopLoading();
        }

        private async Task HandleOpenAddDialogAsync()
        {
            await AddShowProductDialog.ShowAsync();
        }

        private async Task HandleOpenEditDialogAsync(ShowProduct showProduct)
        {
            await EditShowProductDialog.ShowAsync(showProduct);
        }

        private Task HandleShowProductAddedAsync(ShowProduct showProduct)
        {
            ShowProducts.Add(showProduct);
            return Task.CompletedTask;
        }

        private Task HandleShowProductUpdatedAsync(ShowProduct showProduct)
        {
            ShowProducts.RemoveAll(x => x.Id == showProduct.Id);
            ShowProducts.Add(showProduct);

            return Task.CompletedTask;
        }
    }
}
