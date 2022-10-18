using Microsoft.AspNetCore.Components;

namespace FMFT.Web.Client.Views.Shared.Components.Bodies
{
    public partial class AccountLayoutBody
    {
        [Parameter]
        public RenderFragment Header { get; set; }
        [Parameter]
        public RenderFragment Content { get; set; }
        [Parameter]
        public RenderFragment Footer { get; set; }
    }
}