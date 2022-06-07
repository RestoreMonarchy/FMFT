using FMFT.Client.Brokers.JSRuntimes;
using Microsoft.AspNetCore.Components;

namespace FMFT.Client.Views.Components.Tickets
{
    public partial class SeatSelectorComponent
    {
        [Inject]
        public IJSRuntimeBroker JSRuntimeBroker { get; set; }

        public ElementReference ContainerElement { get; set; }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await JSRuntimeBroker.InitializePanzoomElementAsync(ContainerElement);
            }            
        }
    }
}
