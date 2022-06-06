using Microsoft.AspNetCore.Components;

namespace FMFT.Client.Views.Bases.Breacrumbs
{
    public partial class Breadcrumb
    {
        [Parameter]
        public RenderFragment ChildContent { get; set; }

        private readonly List<BreadcrumbItem> items = new();

        public void AddItem(BreadcrumbItem item)
        {
            items.Add(item);
            StateHasChanged();
        }
    }
}
