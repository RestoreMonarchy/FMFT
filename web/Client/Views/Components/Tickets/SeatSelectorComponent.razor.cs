using FMFT.Web.Client.Brokers.JSRuntimes;
using Microsoft.AspNetCore.Components;

namespace FMFT.Web.Client.Views.Components.Tickets
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
