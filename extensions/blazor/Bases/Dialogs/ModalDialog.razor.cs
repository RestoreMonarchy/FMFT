using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace FMFT.Extensions.Blazor.Bases.Dialogs
{
    public partial class ModalDialog
    {
        [Parameter]
        public bool AlignCenter { get; set; }
        [Parameter]
        public bool HideCloseButton { get; set; }
        [Parameter]
        public RenderFragment Title { get; set; }
        [Parameter]
        public RenderFragment Body { get; set; }
        [Parameter]
        public RenderFragment Footer { get; set; }
        [Parameter]
        public ESize Size { get; set; }

        public ElementReference ModalElement { get; set; }

        [Inject]
        public IJSRuntime JSRuntime { get; set; }

        private string GetDialogClasses()
        {
            List<string> classes = new();

            if (AlignCenter)
            {
                classes.Add("modal-dialog-centered");
            }

            switch (Size)
            {
                case ESize.Small:
                    classes.Add("modal-sm");
                    break;
                case ESize.Large:
                    classes.Add("modal-lg");
                    break;
                case ESize.ExtraLarge:
                    classes.Add("modal-xl");
                    break;
            }

            return string.Join(", ", classes);
        }

        public async ValueTask ShowAsync()
        {
            await JSRuntime.InvokeVoidAsync("ShowModal", ModalElement);
        }

        public async ValueTask ShowStaticAsync()
        {
            await JSRuntime.InvokeVoidAsync("ShowModalStatic", ModalElement);
        }

        public async ValueTask HideAsync()
        {
            await JSRuntime.InvokeVoidAsync("HideModal", ModalElement);
        }

        public enum ESize
        {
            Default = 0,
            Small = 1,
            Large = 2,
            ExtraLarge = 3
        }
    }
}
