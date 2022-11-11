using Microsoft.AspNetCore.Components;

namespace FMFT.Web.Client.Views.Shared.Components.Bodies
{
    public partial class MainLayoutBody
    {
        [Parameter]
        public RenderFragment ChildContent { get; set; }
    }
}
