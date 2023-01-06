using FMFT.Extensions.Blazor.Bases.Loadings;
using FMFT.Web.Client.Models.API;
using FMFT.Web.Client.Models.API.ShowProducts;
using FMFT.Web.Client.Models.API.Shows;
using Microsoft.AspNetCore.Components;

namespace FMFT.Web.Client.Views.Pages.Admin.Shows.Subpages
{
    public partial class ProductsShowAdminSubpage
    {
        [Parameter]
        public Show Show { get; set; }

        public LoadingView LoadingView { get; set; }

        public APIResponse<List<ShowProduct>> ShowProductsResponse { get; set; }

        public List<ShowProduct> ShowProducts => ShowProductsResponse.Object;

        protected override async Task OnParametersSetAsync()
        {
            ShowProductsResponse = await APIBroker.GetShowProductsByShowIdAsync(Show.Id);

            LoadingView.StopLoading();
        }
    }
}
