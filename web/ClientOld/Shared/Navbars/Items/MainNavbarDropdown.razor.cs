using Microsoft.AspNetCore.Components;

namespace FMFT.Web.Client.Shared.Navbars.Items
{
    public partial class MainNavbarDropdown
    {
        [Parameter]
        public string Value { get; set; }
        [Parameter]
        public RenderFragment ChildContent { get; set; }

        public List<MainNavbarDropdownItem> Items { get; set; } = new List<MainNavbarDropdownItem>();

        public void AddItem(MainNavbarDropdownItem item)
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
