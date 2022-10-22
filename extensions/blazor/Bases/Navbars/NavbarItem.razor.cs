using Microsoft.AspNetCore.Components;

namespace FMFT.Extensions.Blazor.Bases.Navbars
{
    public partial class NavbarItem
    {
        [Parameter]
        public string Location { get; set; }
        [Parameter]
        public bool StartsWith { get; set; }
        [Parameter]
        public RenderFragment ChildContent { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        protected string CurrentLocation => NavigationManager.ToBaseRelativePath(NavigationManager.Uri);

        protected string LocationHref => Location ?? "javascript:void(0)";

        public bool IsActive()
        {
            if (string.IsNullOrEmpty(Location))
            {
                return false;
            }

            string location = Location.TrimStart('/');

            if (location == string.Empty)
            {
                if (CurrentLocation == string.Empty)
                {
                    return true;
                }

                return false;
            }

            if (StartsWith)
            {
                return CurrentLocation.StartsWith(location, StringComparison.OrdinalIgnoreCase);
            } else
            {
                return CurrentLocation.Equals(location, StringComparison.OrdinalIgnoreCase);
            }
        }

        protected string GetActiveClass()
        {
            return IsActive() ? "active" : string.Empty;
        }
    }
}
