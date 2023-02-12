using Microsoft.AspNetCore.Components;

namespace FMFT.Extensions.Blazor.Bases.Navigations
{
    public partial class Navigation
    {
        public async Task HandleClickAsync(NavigationItem item)
        {
            if (item == ActiveNavigationItem)
            {
                return;
            }

            await OnNavigate.InvokeAsync(item);
        }

        public async Task HandleSelectAsync(ChangeEventArgs args)
        {
            Guid itemId = Guid.Parse(args.Value.ToString());
            NavigationItem item = Items.First(x => x.ID == itemId);

            if (item == ActiveNavigationItem)
            {
                return;
            }

            await OnNavigate.InvokeAsync(item);
        }
    }
}
