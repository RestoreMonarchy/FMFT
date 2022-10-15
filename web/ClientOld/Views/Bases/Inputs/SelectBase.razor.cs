using Microsoft.AspNetCore.Components;

namespace FMFT.Web.Client.Views.Bases.Inputs
{
    public partial class SelectBase
    {
        [Parameter]
        public int Value { get; set; }

        [Parameter]
        public EventCallback<int> ValueChanged { get; set; }

        [Parameter]
        public string Class { get; set; }
        [Parameter]
        public RenderFragment ChildContent { get; set; }

        public bool IsEnabled => IsDisabled is false;

        protected Task OnValueChanged(ChangeEventArgs changeEventArgs)
        {
            this.Value = int.Parse(changeEventArgs.Value.ToString());

            return ValueChanged.InvokeAsync(this.Value);
        }

        public Task SetValueAsync(int value) =>
        InvokeAsync(async () =>
        {
            this.Value = value;
            await this.ValueChanged.InvokeAsync(this.Value);
        });
    }
}
