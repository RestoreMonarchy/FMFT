using Microsoft.AspNetCore.Components;

namespace FMFT.Web.Client.Views.Bases.Buttons
{
    public partial class ButtonBase : ComponentBase
    {
        [Parameter]
        public RenderFragment ChildContent { get; set; }

        [Parameter]
        public EventCallback OnClick { get; set; }

        [Parameter]
        public bool IsDisabled { get; set; }

        [Parameter]
        public bool IsSpinning { get; set; }

        [Parameter]
        public string SpinningText { get; set; }

        [Parameter]
        public string Class { get; set; }

        public void Click() => InvokeAsync(OnClick.InvokeAsync);

        public void Disable()
        {
            this.IsDisabled = true;
            InvokeAsync(StateHasChanged);
        }

        public void Enable()
        {
            this.IsDisabled = false;
            InvokeAsync(StateHasChanged);
        }

        public void StartSpinning()
        {
            this.IsSpinning = true;
            InvokeAsync(StateHasChanged);
        }

        public void StopSpinning()
        {
            this.IsSpinning = false;
            InvokeAsync(StateHasChanged);
        }
    }
}
