using Microsoft.AspNetCore.Components;

namespace FMFT.Extensions.Blazor.Bases.Breacrumbs
{
    public partial class BreadcrumbItem
    {
        [CascadingParameter]
        public Breadcrumb Breadcrumb { get; set; }

        [Parameter]
        public RenderFragment ChildContent { get; set; }
        [Parameter]
        public bool Active { get; set; }
        [Parameter]
        public string Link { get; set; }
        [Parameter]
        public string Class { get; set; }

        public string ActiveClass => Active ? "active" : "";
        public bool HasLink => !string.IsNullOrEmpty(Link);

        protected override void OnAfterRender(bool firstRender)
        {
            if (!firstRender)
                return;

            Breadcrumb.AddItem(this);
        }
    }
}
