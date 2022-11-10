using FMFT.Extensions.Blazor.Bases.Dialogs;
using Microsoft.AspNetCore.Components;

namespace FMFT.Web.Client.Views.Shared.Components.Dialogs
{
    public partial class MediaPreviewDialog
    {
        [Parameter]
        public RenderFragment Title { get; set; }

        public ModalDialog ModalDialog { get; set; }

        private Guid? mediaId = Guid.Empty;

        public async ValueTask ShowMediaAsync(Guid? mediaId)
        {
            this.mediaId = mediaId;
            await ModalDialog.ShowAsync();
            StateHasChanged();
        }
    }
}
