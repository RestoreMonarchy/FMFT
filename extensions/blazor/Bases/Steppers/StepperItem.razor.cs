using Microsoft.AspNetCore.Components;

namespace FMFT.Extensions.Blazor.Bases.Steppers
{
    public partial class StepperItem
    {
        [Parameter]
        public string Text { get; set; }
        [Parameter]
        public bool Active { get; set; }
        [Parameter]
        public bool Disabled { get; set; }
        [Parameter]
        public string Url { get; set; }

        [CascadingParameter]
        public Stepper Stepper { get; set; }

        protected override void OnAfterRender(bool firstRender)
        {
            if (!firstRender)
                return;

            Stepper.AddItem(this);
        }

        private string GetClasses()
        {
            List<string> classes = new();

            if (Active)
            {
                classes.Add("border-2");
                classes.Add("border-dark");
                classes.Add("fw-bold");
                classes.Add("text-black");
            } else if (Disabled)
            {
                classes.Add("disabled");
                classes.Add("text-muted");
            } else
            {
                classes.Add("border-1");
                classes.Add("border-dark");
                classes.Add("text-black");
                classes.Add("cursor-pointer");
            }

            return string.Join(' ', classes);
        }
    }
}
