using FMFT.Extensions.Blazor.Bases.Forms;
using Microsoft.AspNetCore.Components;

namespace FMFT.Extensions.Blazor.Bases.Inputs
{
    public class InputBase : ComponentBase
    {
        [Parameter]
        public bool IsDisabled { get; set; }

        [CascadingParameter]
        public FormBase Form { get; set; }

        protected override void OnAfterRender(bool firstRender)
        {
            if (firstRender && Form != null)
            {
                Form.AddInput(this);
            }                
        }

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
    }
}
