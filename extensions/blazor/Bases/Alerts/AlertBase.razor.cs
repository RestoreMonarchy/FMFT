using Microsoft.AspNetCore.Components;

namespace FMFT.Extensions.Blazor.Bases.Alerts
{
    public partial class AlertBase
    {
        [CascadingParameter]
        public AlertGroupBase AlertGroup { get; set; }
        [Parameter]
        public string Class { get; set; }

        [Parameter]
        public RenderFragment ChildContent { get; set; }

        protected override void OnAfterRender(bool firstRender)
        {
            if (firstRender && AlertGroup != null)
            {
                AlertGroup.AddAlert(this);
            }
        }

        private bool showAlert;

        public void Show()
        {
            showAlert = true;
            InvokeAsync(StateHasChanged);
        }

        public void Hide()
        {
            showAlert = false;
            InvokeAsync(StateHasChanged);
        }
    }
}
