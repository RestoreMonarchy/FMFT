using Microsoft.AspNetCore.Components;

namespace FMFT.Extensions.Blazor.Bases.Inputs
{
    public partial class CheckboxInputBase : InputBase
    {
        [Parameter]
        public RenderFragment ChildContent { get; set; }

        [Parameter]
        public bool Value { get; set; }

        [Parameter]
        public EventCallback<bool> ValueChanged { get; set; }

        [Parameter]
        public string Class { get; set; }

        private Task OnValueChanged(ChangeEventArgs changeEventArgs)
        {
            this.Value = bool.Parse(changeEventArgs.Value.ToString());
            
            return ValueChanged.InvokeAsync(this.Value);
        }

        public Task SetValueAsync(bool value) =>
        InvokeAsync(async () =>
        {
            this.Value = value;
            await this.ValueChanged.InvokeAsync(this.Value);
        });
    }
}
