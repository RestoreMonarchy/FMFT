﻿using Microsoft.AspNetCore.Components;

namespace FMFT.Extensions.Blazor.Bases.Navigations
{
    public class NavigationBase : ComponentBase
    {
        [Parameter]
        public RenderFragment ChildContent { get; set; }
        [Parameter]
        public string ActiveKey { get; set; }

        [Parameter]
        public EventCallback<NavigationItem> OnNavigate { get; set; }

        public List<NavigationItem> Items { get; set; } = new();

        protected override void OnParametersSet()
        {
            SetActiveNavigationItem(ActiveKey);
        }

        private void SetActiveNavigationItem(string key)
        {
            NavigationItem navigationItem = Items.FirstOrDefault(x => x.Key == key);

            if (navigationItem == null)
            {
                navigationItem = Items.FirstOrDefault();
            }

            if (navigationItem == ActiveNavigationItem)
            {
                return;
            }

            if (ActiveNavigationItem != null)
            {
                ActiveNavigationItem.SetIsActive(false);
            }

            navigationItem.SetIsActive(true);
            ActiveNavigationItem = navigationItem;
        }

        public void AddItem(NavigationItem item)
        {
            Items.Add(item);

            if (string.Equals(ActiveKey, item.Key))
            {
                SetActiveNavigationItem(ActiveKey);
            }
        }

        public NavigationItem ActiveNavigationItem { get; set; }
        public Guid ActiveItemId => ActiveNavigationItem?.ID ?? Guid.Empty;
    }
}
