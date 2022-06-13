using FMFT.Web.Client.Views.Bases.Forms;
using Microsoft.AspNetCore.Components;

namespace FMFT.Web.Client.Views.Bases.Inputs
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
