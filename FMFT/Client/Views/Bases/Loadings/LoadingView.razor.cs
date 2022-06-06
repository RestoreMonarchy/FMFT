using Microsoft.AspNetCore.Components;

namespace FMFT.Client.Views.Bases.Loadings
{
    public partial class LoadingView
    {
        [Parameter]
        public object State { get; set; }

        [Parameter]
        public RenderFragment ChildContent { get; set; }

        private bool CheckState()
        {
            if (State == default)
                return false;

            return true;
        }
    }
}