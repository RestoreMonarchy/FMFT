using Microsoft.AspNetCore.Components;

namespace FMFT.Extensions.Blazor.Bases.Steppers
{
    public partial class StepperItem
    {
        [Parameter]
        public string Text { get; set; }
        [Parameter]
        public RenderFragment ChildContent { get; set; }

        [CascadingParameter]
        public Stepper Stepper { get; set; }

        protected override void OnAfterRender(bool firstRender)
        {
            if (!firstRender)
                return;

            Stepper.AddItem(this);
        }

        public bool IsActive { get; set; }
        public bool IsDisabled { get; set; }

        public void SetActive(bool active)
        {
            IsActive = active;
            InvokeAsync(StateHasChanged);
        }

        public void SetDisabled(bool disabled)
        {
            IsDisabled = disabled;
            InvokeAsync(StateHasChanged);
        }

        private Task HandleClickAsync()
        {
            if (Stepper.IsPast(this))
            {
                Stepper.Step(this);
            }            
            return Task.CompletedTask;
        }

        private string GetClasses()
        {
            List<string> classes = new();

            bool isPast = Stepper.IsPast(this);

            if (IsActive)
            {
                classes.Add("border-2");
                classes.Add("border-dark");
                classes.Add("fw-bold");
            } else
            {
                if (isPast)
                {
                    classes.Add("text-black");
                    classes.Add("border-dark");
                } else
                {
                    classes.Add("text-muted");
                }                
            }

            if (IsDisabled)
            {
                classes.Add("disabled");
            } else
            {
                if (isPast)
                {
                    classes.Add("cursor-pointer");
                }                
            }

            return string.Join(' ', classes);
        }
    }
}
