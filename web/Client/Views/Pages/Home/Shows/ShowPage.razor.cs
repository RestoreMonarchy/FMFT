using FMFT.Extensions.Blazor.Bases.Loadings;
using FMFT.Extensions.Blazor.Bases.MarkdownEditors;
using FMFT.Web.Client.Models.API;
using FMFT.Web.Client.Models.API.Auditoriums;
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

        public APIResponse<Show> ShowResponse { get; set; }
        public APIResponse<Auditorium> AuditoriumResponse { get; set; }
        public APIResponse<List<ShowGallery>> ShowGalleryResponse { get; set; }

        public Show Show => ShowResponse.Object;
        public Auditorium Auditorium => AuditoriumResponse.Object;
        public List<ShowGallery> ShowGallery => ShowGalleryResponse.Object;

        public MarkupString Description => new MarkupString(MarkdownEditorHelper.ParseToHtml(Show.Description));

        protected override async Task OnParametersSetAsync()
        {
            Task[] getDataTasks = { 
                                    GetShowResponseAsync(), 
                                    GetAuditoriumResponseAsync(), 
                                    GetShowGalleryResponseAsync() 
                                  };

            await Task.WhenAll(getDataTasks);

            LoadingView.StopLoading();
        }

        private async Task GetShowResponseAsync()
        {
            ShowResponse = await APIBroker.GetShowByIdAsync(ShowId);
        }
        private async Task GetAuditoriumResponseAsync()
        {
            AuditoriumResponse = await APIBroker.GetAuditoriumByShowIdAsync(ShowId);
        }

        private async Task GetShowGalleryResponseAsync()
        {
            ShowGalleryResponse = await APIBroker.GetShowGalleryByShowIdAsync(ShowId);
        }
    }
}
