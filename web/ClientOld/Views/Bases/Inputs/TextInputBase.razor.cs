using Microsoft.AspNetCore.Components;

namespace FMFT.Web.Client.Views.Bases.Inputs
{
    public partial class TextInputBase
    {
        [Parameter]
        public string Value { get; set; }

        [Parameter]
        public string Placeholder { get; set; }

        [Parameter]
        public EventCallback<string> ValueChanged { get; set; }

        [Parameter]
        public string Class { get; set; }

        public bool IsEnabled => IsDisabled is false;

        protected Task OnValueChanged(ChangeEventArgs changeEventArgs)
        {
            this.Value = changeEventArgs.Value.ToString();

            return ValueChanged.InvokeAsync(this.Value);
        }

        public Task SetValueAsync(string value) =>
        InvokeAsync(async () =>
        {
            this.Value = value;
            await this.ValueChanged.InvokeAsync(this.Value);
        });
    }
}
