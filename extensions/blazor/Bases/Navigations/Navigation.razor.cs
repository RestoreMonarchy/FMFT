using FMFT.Extensions.Blazor.Bases.Steppers;
using Microsoft.AspNetCore.Components;

namespace FMFT.Extensions.Blazor.Bases.Navigations
{
    public partial class Navigation
    {
        [Parameter]
        public RenderFragment ChildContent { get; set; }
        [Parameter]
        public string ActiveItemUrlId { get; set; }
        [Parameter]
        public EventCallback<NavigationItem> OnNavigate { get; set; }

        public List<NavigationItem> Items { get; set; } = new();

        protected override void OnParametersSet()
        {
            Console.WriteLine("Navigation OnParametersSet");
        }

        protected override void OnAfterRender(bool firstRender)
        {
            Console.WriteLine("Navigation OnAfterRender");
        }

        public async Task AddItem(NavigationItem item)
        {
            Console.WriteLine("Navigation AddItem");

            Items.Add(item);

            if (item.UrlId.Equals(ActiveItemUrlId, StringComparison.OrdinalIgnoreCase))
            {
                await SetActiveItemAsync(item);
            }
        }

        public NavigationItem ActiveNavigationItem { get; set; }
        public Guid ActiveItemId => ActiveNavigationItem?.ID ?? Guid.Empty;

        public async Task SetActiveItemAsync(NavigationItem item)
        {
            if (ActiveNavigationItem == item)
            {
                return;
            }

            ActiveNavigationItem?.SetIsActive(false);
                        
            ActiveNavigationItem = item;
            ActiveNavigationItem.SetIsActive(true);

            await OnNavigate.InvokeAsync(ActiveNavigationItem);
        }

        public async Task HandleClickAsync(NavigationItem item)
        {
            await SetActiveItemAsync(item);
        }

        public async Task HandleSelectAsync(ChangeEventArgs args)
        {
            Guid itemId = Guid.Parse(args.Value.ToString());
            NavigationItem item = Items.First(x => x.ID == itemId);

            await SetActiveItemAsync(item);
        }
    }
}
