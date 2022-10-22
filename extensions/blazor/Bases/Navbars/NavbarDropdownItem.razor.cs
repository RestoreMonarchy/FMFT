using Microsoft.AspNetCore.Components;

namespace FMFT.Extensions.Blazor.Bases.Navbars
{
    public partial class NavbarDropdownItem
    {
        [CascadingParameter]
        public NavbarDropdown Dropdown { get; set; }
        [Parameter]
        public EventCallback OnClick { get; set; }

        protected override void OnAfterRender(bool firstRender)
        {
            if (firstRender && Dropdown != null)
            {
                Dropdown.AddItem(this);
            }
        }

        private async Task HandleClickAsync()
        {
            await OnClick.InvokeAsync();
        }
    }
}
