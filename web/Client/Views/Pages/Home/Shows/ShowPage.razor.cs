using FMFT.Extensions.Blazor.Bases.Alerts;
using FMFT.Extensions.Blazor.Bases.Loadings;
using FMFT.Extensions.Blazor.Bases.MarkdownEditors;
using FMFT.Web.Client.Models.API;
using FMFT.Web.Client.Models.API.Auditoriums;
using FMFT.Web.Client.Models.API.ShowProducts;
using FMFT.Web.Client.Models.API.Shows;
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
            ShowResponse = await APIBroker.GetPublicShowByIdAsync(ShowId);
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
