using FMFT.Extensions.Blazor.Bases.Alerts;
using FMFT.Extensions.Blazor.Bases.Loadings;
using FMFT.Extensions.Blazor.Bases.MarkdownEditors;
using FMFT.Web.Client.Models.API;
using FMFT.Web.Client.Models.API.Auditoriums;
using FMFT.Web.Client.Models.API.ShowProducts;
using FMFT.Web.Client.Models.API.Shows;
using FMFT.Web.Shared.Enums;
using Microsoft.AspNetCore.Components;

namespace FMFT.Web.Client.Views.Pages.Home.Shows
{
    public partial class ShowPage
    {
        [Parameter]
        public int ShowId { get; set; }

        private string ShowName => ShowResponse?.Object?.Name ?? ShowId.ToString();

        private LoadingView LoadingView { get; set; }
        public string ErrorCode { get; set; }

        public APIResponse<Show> ShowResponse { get; set; }
        public APIResponse<Auditorium> AuditoriumResponse { get; set; }
        public APIResponse<List<ShowGallery>> ShowGalleryResponse { get; set; }
        public APIResponse<List<ShowProduct>> ShowProductsResponse { get; set; }

        public Show Show => ShowResponse.Object;
        public Auditorium Auditorium => AuditoriumResponse.Object;
        public List<ShowGallery> ShowGallery => ShowGalleryResponse.Object;
        public List<ShowProduct> ShowProducts => ShowProductsResponse.Object;

        public MarkupString Description => new(MarkdownEditorHelper.ParseToHtml(Show.Description));

        public ShowProduct CheapestShowProduct => ShowProducts.Where(x => x.IsEnabled).OrderBy(x => x.Price).FirstOrDefault();
        public int ShowProductsQuantity => ShowProducts.Where(x => x.IsBulk && x.IsEnabled)
            .Sum(x => Math.Max(x.Quantity, Show.ReservedBulkItems.Count(y => y.ShowProductId == x.Id)));
        public int MaxShowCapacity => Auditorium.Seats.Count + ShowProductsQuantity;
        public int ValidBulkItemsCount => Show.ReservedBulkItems.Count(x => GetShowProduct(x.ShowProductId)?.IsEnabled ?? false);

        private ShowProduct GetShowProduct(int showProductId)
        {
            return ShowProducts.FirstOrDefault(x => x.Id == showProductId);
        } 

        protected override async Task OnParametersSetAsync()
        {
            Task[] getDataTasks = new Task[] 
            { 
                GetShowResponseAsync(), 
                GetAuditoriumResponseAsync(), 
                GetShowGalleryResponseAsync(),
                GetShowProductsResponseAsync()
            };

            await Task.WhenAll(getDataTasks);

            if (!ShowResponse.IsSuccessful)
            {
                ErrorCode = ShowResponse.Error.Code;
            }

            LoadingView.StopLoading();            
        }

        private async Task GetShowResponseAsync()
        {
            if (UserAccountState.IsInRole(UserRole.Admin))
            {
                ShowResponse = await APIBroker.GetShowByIdAsync(ShowId);
            } else
            {
                ShowResponse = await APIBroker.GetPublicShowByIdAsync(ShowId);
            }
        }

        private async Task GetAuditoriumResponseAsync()
        {
            AuditoriumResponse = await APIBroker.GetAuditoriumByShowIdAsync(ShowId);
        }

        private async Task GetShowGalleryResponseAsync()
        {
            ShowGalleryResponse = await APIBroker.GetShowGalleryByShowIdAsync(ShowId);
        }

        private async Task GetShowProductsResponseAsync()
        {
            ShowProductsResponse = await APIBroker.GetShowProductsByShowIdAsync(ShowId);
        }
    }
}
