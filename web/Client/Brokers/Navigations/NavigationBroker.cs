﻿using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;

namespace FMFT.Web.Client.Brokers.Navigations
{
    public class NavigationBroker : INavigationBroker
    {
        private readonly NavigationManager navigationManager;

        public NavigationBroker(NavigationManager navigationManager)
        {
            this.navigationManager = navigationManager;
            navigationManager.LocationChanged += TriggerLocationChanged;
        }

        public string Uri => navigationManager.Uri;
        public string AbsoluteUri => navigationManager.ToAbsoluteUri(Uri).ToString();
        public string RelativeUri => navigationManager.ToBaseRelativePath(Uri);

        public event Action<LocationChangedEventArgs> OnLocationChange;
        private void TriggerLocationChanged(object sender, LocationChangedEventArgs e)
        {
            OnLocationChange?.Invoke(e);
        }

        public void ForceLoadNavigateTo(string url)
        {
            navigationManager.NavigateTo(url, true);
        }

        public void NavigateTo(string url)
        {
            navigationManager.NavigateTo(url);
        }
    }
}
