using FMFT.Web.Client.Views.Bases.Forms;
using Microsoft.AspNetCore.Components;

namespace FMFT.Web.Client.Views.Bases.Validations
{
    public partial class ValidationMessageBase
    {
        [Parameter]
        public string Key { get; set; }

        [Parameter]
        public RenderFragment ChildContent { get; set; }

        [CascadingParameter]
        public FormBase Form { get; set; }

        public bool IsDisabled { get; set; } = true;
        public bool IsEnabled => !IsDisabled;

        public string[] Errors { get; set; }

        protected override void OnAfterRender(bool firstRender)
        {
            if (firstRender && Form != null)
            {
                Form.AddValidationMessage(this);
            }
        }

        public void Disable()
        {
            this.IsDisabled = true;
            InvokeAsync(StateHasChanged);
        }

        public void Enable(string[] errors)
        {
            Errors = errors;
            this.IsDisabled = false;
            InvokeAsync(StateHasChanged);
        }
    }
}
