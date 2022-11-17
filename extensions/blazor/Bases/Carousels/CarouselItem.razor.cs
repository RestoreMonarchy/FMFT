using Microsoft.AspNetCore.Components;
using System.ComponentModel;

namespace FMFT.Extensions.Blazor.Bases.Carousels
{
    public partial class CarouselItem
    {
        [CascadingParameter]
        public Carousel Carousel { get; set; }

        [Parameter]
        public RenderFragment ChildContent { get; set; }

        protected override void OnAfterRender(bool firstRender)
        {
            if (!firstRender)
            {
                return;
            }

            Carousel.AddItem(this);
        }
    }
}
