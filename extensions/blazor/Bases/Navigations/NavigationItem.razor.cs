using Microsoft.AspNetCore.Components;

namespace FMFT.Extensions.Blazor.Bases.Navigations
{
    public partial class NavigationItem
    {
        [Parameter]
        public string Text { get; set; }
        [Parameter]
        public RenderFragment ChildContent { get; set; }
        [Parameter]
        public bool Active { get; set; }

        [CascadingParameter]
        public Navigation Navigation { get; set; }

        public Guid ID { get; } = Guid.NewGuid();

        protected override void OnAfterRender(bool firstRender)
        {
            if (!firstRender)
                return;

            IsActive = Active;
            Navigation.AddItem(this);
        }

        public bool IsActive { get; set; }

        public void SetIsActive(bool isActive)
        {
            IsActive = isActive;
        }

        public string GetActiveClass()
        {
            return IsActive ? "active" : string.Empty;
        }

    }
}
