using FMFT.Extensions.Blazor.Bases.Buttons;
using FMFT.Extensions.Blazor.Bases.Dialogs;
using FMFT.Extensions.Blazor.Bases.Loadings;
using FMFT.Web.Client.Models.API;
using FMFT.Web.Client.Models.API.Shows;
using FMFT.Web.Client.Models.API.Shows.Requests;
using Microsoft.AspNetCore.Components;

namespace FMFT.Web.Client.Views.Pages.Admin.Shows.Subpages
{
    public partial class GalleryShowAdminSubpage
    {
        [Parameter]
        public Show Show { get; set; }
        
        public LoadingView LoadingView { get; set; }
        public ModalDialog PreviewShowGalleryModalDialog { get; set; }
        public ModalDialog DeleteShowGalleryModalDialog { get; set; }
        public ButtonBase DeleteShowGalleryButton { get; set; }

        public APIResponse<List<ShowGallery>> ShowGalleryResponse { get; set; }

        public List<ShowGallery> ShowGallery => ShowGalleryResponse.Object;

        protected override async Task OnInitializedAsync()
        {
            ShowGalleryResponse = await APIBroker.GetShowGalleryByShowIdAsync(Show.Id);

            LoadingView.StopLoading();
        }

        private ShowGallery previewShowGallery = null;
        private ShowGallery deleteShowGallery = null;

        private async Task HandleShowGalleryPreviewAsync(ShowGallery showGallery)
        {
            previewShowGallery = showGallery;
            await PreviewShowGalleryModalDialog.ShowAsync();
        }

        private async Task HandleShowGalleryMediaAddAsync(Guid? mediaId)
        {
            if (!mediaId.HasValue)
            {
                return;
            }

            AddShowGalleryRequest request = new()
            {
                ShowId = Show.Id,
                MediaId = mediaId.Value
            };

            APIResponse showGalleryResponse = await APIBroker.AddShowGalleryAsync(request);

            if (!showGalleryResponse.IsSuccessful)
            {
                return;
            }

            ShowGallery showGallery = await showGalleryResponse.ReadFromJsonAsync<ShowGallery>();
            ShowGallery.Add(showGallery);
        }

        private async Task HandleShowGalleryDeleteModalAsync(ShowGallery showGallery)
        {
            deleteShowGallery = showGallery;
            await DeleteShowGalleryModalDialog.ShowAsync();
        }

        private async Task HandleDeleteShowGalleryAsync()
        {
            DeleteShowGalleryButton.StartSpinning();

            APIResponse response = await APIBroker.DeleteShowGalleryByIdAsync(deleteShowGallery.Id);
            if (response.IsSuccessful)
            {
                ShowGallery.Remove(deleteShowGallery);
            }

            await DeleteShowGalleryModalDialog.HideAsync();
            deleteShowGallery = null;

            DeleteShowGalleryButton.StopSpinning();
        }
    }
}
