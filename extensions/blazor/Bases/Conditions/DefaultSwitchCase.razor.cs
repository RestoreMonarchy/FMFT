using Microsoft.AspNetCore.Components;

namespace FMFT.Extensions.Blazor.Bases.Conditions
{
    public partial class DefaultSwitchCase
    {
        [Parameter]
        public RenderFragment ChildContent { get; set; }
        
        [CascadingParameter]
        public Switch Switch { get; set; }

        protected override void OnAfterRender(bool firstRender)
        {
            if (firstRender)
            {
                Switch.SetDefaultCase(this);
            }
        }
    }
}
