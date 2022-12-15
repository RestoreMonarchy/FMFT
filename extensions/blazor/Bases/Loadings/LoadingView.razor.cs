using Microsoft.AspNetCore.Components;

namespace FMFT.Extensions.Blazor.Bases.Loadings
{
    public partial class LoadingView
    {
        [Parameter]
        public RenderFragment ChildContent { get; set; }

        [Parameter]
        public bool IsLoading { get; set; } = true;
        [Parameter]
        public bool IsHidden { get; set; } = false;

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

        public void Hide()
        {
            IsHidden = true;
            InvokeAsync(StateHasChanged);
        }

        public void Show()
        {
            IsHidden = false;
            InvokeAsync(StateHasChanged);
        }
    }
}