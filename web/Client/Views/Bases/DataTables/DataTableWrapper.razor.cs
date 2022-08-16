using Microsoft.AspNetCore.Components;

namespace FMFT.Web.Client.Views.Bases.DataTables
{
    public partial class DataTableWrapper<TItem>
    {
        [Parameter]
        public IEnumerable<TItem> Data { get; set; }
        [Parameter]
        public RenderFragment ChildContent { get; set; }
    }
}
