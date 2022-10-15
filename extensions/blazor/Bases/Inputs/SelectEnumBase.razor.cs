using Microsoft.AspNetCore.Components;

namespace FMFT.Extensions.Blazor.Bases.Inputs
{
    public partial class SelectEnumBase<TEnum> where TEnum : struct
    {
        [Parameter]
        public TEnum Value { get; set; }

        [Parameter]
        public EventCallback<TEnum> ValueChanged { get; set; }

        [Parameter]
        public string Class { get; set; }
        [Parameter]
        public RenderFragment ChildContent { get; set; }

        public bool IsEnabled => IsDisabled is false;

        protected Task OnValueChanged(ChangeEventArgs changeEventArgs)
        {
            this.Value = Enum.Parse<TEnum>(changeEventArgs.Value.ToString(), true);

            return ValueChanged.InvokeAsync(this.Value);
        }

        public Task SetValueAsync(TEnum value) =>
        InvokeAsync(async () =>
        {
            this.Value = value;
            await this.ValueChanged.InvokeAsync(this.Value);
        });
    }
}
