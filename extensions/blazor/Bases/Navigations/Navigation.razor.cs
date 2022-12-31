using FMFT.Extensions.Blazor.Bases.Steppers;
using Microsoft.AspNetCore.Components;

namespace FMFT.Extensions.Blazor.Bases.Navigations
{
    public partial class Navigation
    {
        [Parameter]
        public RenderFragment ChildContent { get; set; }

        public List<NavigationItem> Items { get; set; } = new();

        public void AddItem(NavigationItem item)
        {
            Items.Add(item);

            if (item.IsActive)
            {
                SetActiveItem(item);   
            }
        }

        public NavigationItem ActiveItem { get; set; }
        public Guid ActiveItemId => ActiveItem?.ID ?? Guid.Empty;

        public void SetActiveItem(NavigationItem item)
        {
            if (ActiveItem == item)
            {
                return;
            }

            ActiveItem?.SetIsActive(false);
            
            ActiveItem = item;
            ActiveItem.SetIsActive(true);

            StateHasChanged();
        }

        public Task HandleClickAsync(NavigationItem item)
        {
            SetActiveItem(item);

            return Task.CompletedTask;
        }

        public Task HandleSelectAsync(ChangeEventArgs args)
        {
            Guid itemId = Guid.Parse(args.Value.ToString());
            NavigationItem item = Items.First(x => x.ID == itemId);

            SetActiveItem(item);

            return Task.CompletedTask;
        }
    }
}
