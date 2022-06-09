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

        private string CurrentLocation => NavigationBroker.RelativeUri;

        private string GetActiveClass()
        {
            string location = Location.TrimStart('/');

            if (location == string.Empty)
            {
                if (CurrentLocation == string.Empty)
                {
                    return "active";
                }
                return string.Empty;
            }

            if (CurrentLocation.StartsWith(location, StringComparison.OrdinalIgnoreCase))
            {
                return "active";
            }
            return string.Empty;
        }
    }
}
