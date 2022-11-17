using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace FMFT.Extensions.Blazor.Bases.Carousels
{
    public partial class Carousel
    {
        [Parameter]
        public RenderFragment ChildContent { get; set; }

        [Inject]
        public IJSRuntime JsRuntime { get; set; }

        private string carouselId = "carousel-" + Guid.NewGuid().ToString().Replace("-", "").Substring(0, 8);

        public ElementReference CarouselElement { get; set; }

        private List<CarouselItem> items = new();

        public void AddItem(CarouselItem item)
        {
            items.Add(item);
            StateHasChanged();
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (!firstRender)
            {
                return;
            }

            await JsRuntime.InvokeVoidAsync("StartCarousel", CarouselElement);
        }
    }
}
