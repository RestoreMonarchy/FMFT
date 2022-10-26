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

            if (ActiveItem == null)
            {
                Step(item);
            }
        }

        public bool IsPast(StepperItem stepperItem)
        {
            int activeIndex = Items.IndexOf(ActiveItem);
            int index = Items.IndexOf(stepperItem);

            return activeIndex > index;
        }

        public void StepUp()
        {
            int index = Items.IndexOf(ActiveItem);
            if (index >= Items.Count - 1)
            {
                // Already last item in the list
                return;
            }

            StepperItem stepperItem = Items[index + 1];
            Step(stepperItem);
        }

        public void StepDown()
        {
            int index = Items.IndexOf(ActiveItem);
            if (index <= 0)
            {
                // Already last item in the list
                return;
            }

            StepperItem stepperItem = Items[index - 1];
            Step(stepperItem);
        }

        public void Step(StepperItem stepperItem)
        {
            if (ActiveItem != null)
            {
                ActiveItem.SetActive(false);
            }
            
            ActiveItem = stepperItem;
            ActiveItem.SetActive(true);
            InvokeAsync(StateHasChanged);
        }

        public void LockPast()
        {
            foreach (StepperItem stepperItem in Items)
            {
                if (!IsPast(stepperItem))
                {
                    continue;
                }

                stepperItem.SetDisabled(true);
            }
        }

        public StepperItem ActiveItem { get; set; }
    }
}
