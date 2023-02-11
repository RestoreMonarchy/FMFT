using Microsoft.AspNetCore.Components;

namespace FMFT.Extensions.Blazor.Bases.Steppers
{
    public partial class Stepper
    {
        [Parameter]
        public RenderFragment ChildContent { get; set; }

        public List<StepperItem> Items { get; set; } = new();

        public void AddItem(StepperItem item)
        {
            Items.Add(item);
        }
    }
}
