using Microsoft.AspNetCore.Components;

namespace FMFT.Web.Client.Shared.Navbars.Items
{
    public partial class MainNavbarDropdownItem
    {
        [CascadingParameter]
        public MainNavbarDropdown Dropdown { get; set; }

        protected override void OnAfterRender(bool firstRender)
        {
            if (firstRender && Dropdown != null)
            {
                Dropdown.AddItem(this);
            }
        }
    }
}
