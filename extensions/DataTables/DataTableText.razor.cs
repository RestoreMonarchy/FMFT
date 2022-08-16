using Microsoft.AspNetCore.Components;

namespace FMFT.Extensions.DataTables
{
    public partial class DataTableText<TItem>
    {
        [CascadingParameter(Name = "DataTable")]
        public DataTable<TItem> DataTable { get; set; }

        protected override void OnAfterRender(bool firstRender)
        {
            if (firstRender)
                DataTable.UpdateText(this);
        }

        [Parameter]
        public RenderFragment SearchText { get; set; }
        [Parameter]
        public string SearchPlaceholder { get; set; }
        [Parameter]
        public RenderFragment DescendingIcon { get; set; }
        [Parameter]
        public RenderFragment AscendingIcon { get; set; }
        [Parameter]
        public RenderFragment NoIcon { get; set; }
    }
}
