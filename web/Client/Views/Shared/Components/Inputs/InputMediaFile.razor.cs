using FMFT.Web.Client.Models.API;
using FMFT.Web.Client.Models.API.Medias;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace FMFT.Web.Client.Views.Shared.Components.Inputs
{
    public partial class InputMediaFile
    {
        [Parameter]
        public string Class { get; set; }
        [Parameter]
        public string Accept { get; set; }

        [Parameter]
        public Guid? Value { get; set; }
        [Parameter]
        public EventCallback<Guid?> ValueChanged { get; set; }

        private async Task HandleOnFileChangeAsync(InputFileChangeEventArgs args)
        {
            await InvokeAsync(async () => 
            {
                await ProcessFileAsync(args.File);                
            });
        }

        private async ValueTask ProcessFileAsync(IBrowserFile browserFile)
        {
            if (browserFile == null)
            {
                await UpdateValueAsync(Value);
            }

            APIResponse response = await APIBroker.UploadMediaAsync(browserFile);
            response.ThrowIfError();
            Media media = await response.ReadFromJsonAsync<Media>();
            await UpdateValueAsync(media.Id);
        }

        private async Task UpdateValueAsync(Guid? value)
        {
            Value = value;
            await ValueChanged.InvokeAsync(Value);
        }
    }
}
