using FMFT.Web.Client.Brokers.Navigations;
using Microsoft.AspNetCore.Components;

namespace FMFT.Web.Client.Shared.Navbars.Items
{
    public partial class MainNavbarItem
    {
        [Parameter]
        public string Location { get; set; }
        [Parameter]
        public RenderFragment ChildContent { get; set; }

        [Inject]
        public INavigationBroker NavigationBroker { get; set; }

        protected string CurrentLocation => NavigationBroker.RelativeUri;

        public bool IsActive()
        {
            string location = Location.TrimStart('/');

            if (location == string.Empty)
            {
                if (CurrentLocation == string.Empty)
                {
                    return true;
                }

                return false;
            }

            if (CurrentLocation.EndsWith(location, StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }

            return false;
        }

        protected string GetActiveClass()
        {
            return IsActive() ? "active" : string.Empty;
        }
    }
}
