using FMFT.Extensions.Blazor.Bases.Buttons;
using FMFT.Extensions.Blazor.Bases.Dialogs;
using FMFT.Web.Client.Models.API.Medias;
using Microsoft.AspNetCore.Components;

namespace FMFT.Web.Client.Views.Shared.Components.Dialogs
{
    public partial class ImageMediaDialog
    {
        [Parameter]
        public Guid? MediaId { get; set; }
        [Parameter]
        public EventCallback<Guid?> MediaIdChanged { get; set; }

        [Parameter]
        public List<Media> Media { get; set; }

        public ModalDialog MediaModalDialog { get; set; }

        private string GetClasses(Media media)
        {
            List<string> classes = new();

            if (MediaId == media.Id)
            {
                classes.Add("border border-1 border-danger");
            } else
            {
                classes.Add("border border-1");
            }

            return string.Join(" ", classes);
        }

        private async Task HandleSelectMediaAsync(Media media)
        {
            MediaId = media.Id;
        }

        private async Task HandleSubmitAsync()
        {
            await MediaIdChanged.InvokeAsync();
            await MediaModalDialog.HideAsync();
        }
    }
}
