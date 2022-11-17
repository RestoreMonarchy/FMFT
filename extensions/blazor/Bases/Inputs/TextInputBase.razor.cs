using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace FMFT.Extensions.Blazor.Bases.Inputs
{
    public partial class TextInputBase
    {
        [Inject]
        public IJSRuntime JsRuntime { get; set; }

        [Parameter]
        public string Value { get; set; }

        [Parameter]
        public string Placeholder { get; set; }

        [Parameter]
        public EventCallback<string> ValueChanged { get; set; }

        [Parameter]
        public string Class { get; set; }

        public bool IsEnabled => IsDisabled is false;
        public ElementReference InputElement { get; set; }

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

        public async Task SelectAsync()
        {
            await JsRuntime.InvokeVoidAsync("SelectInput", InputElement);
        }
    }
}
