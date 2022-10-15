using Microsoft.AspNetCore.Components;

namespace FMFT.Extensions.Blazor.Bases.Navbars
{
    public partial class NavbarDropdown
    {
        [Parameter]
        public string Value { get; set; }
        [Parameter]
        public RenderFragment ChildContent { get; set; }

        public List<NavbarDropdownItem> Items { get; set; } = new List<NavbarDropdownItem>();

        public void AddItem(NavbarDropdownItem item)
        {
            Items.Add(item);
            InvokeAsync(StateHasChanged);
        }

        private bool IsActive()
        {
            return Items.Any(x => x.IsActive());
        }

        private string GetActiveClass()
        {
            return IsActive() ? "active" : string.Empty;
        }
    }
}
