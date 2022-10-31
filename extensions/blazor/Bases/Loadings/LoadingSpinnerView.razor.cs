using Microsoft.AspNetCore.Components;

namespace FMFT.Extensions.Blazor.Bases.Loadings
{
    public partial class LoadingSpinnerView
    {
        [Parameter]
        public RenderFragment ChildContent { get; set; }

        public bool IsLoading { get; set; } = true;

        public void StartLoading()
        {
            IsLoading = true;
            InvokeAsync(StateHasChanged);
        }

        public void StopLoading()
        {
            IsLoading = false;
            InvokeAsync(StateHasChanged);
        }
    }
}
