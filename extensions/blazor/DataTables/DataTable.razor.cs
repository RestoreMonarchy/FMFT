using Microsoft.AspNetCore.Components;

namespace FMFT.Extensions.Blazor.DataTables
{
    public partial class DataTable<TItem>
    {
        [Parameter]
        public IEnumerable<TItem> Data { get; set; }

        [Parameter]
        public RenderFragment ChildContent { get; set; }

        protected override void OnInitialized()
        {
            searchTimer = new(420)
            {
                AutoReset = false
            };
            searchTimer.Elapsed += OnSearchTimeElapsed;
        }

        protected override void OnParametersSet()
        {
            if (Data != null)
                RefreshItems();
        }

        private List<DataTableColumn<TItem>> Columns { get; set; } = new List<DataTableColumn<TItem>>();

        public void AddColumn(DataTableColumn<TItem> column)
        {
            Columns.Add(column);
            if (CurrentOrder.Column == null)
            {
                CurrentOrder.Column = column;
                RefreshItems();
            }
        }

        public DataTableTitle<TItem> Title { get; private set; }
        public void UpdateTitle(DataTableTitle<TItem> title)
        {
            Title = title;
            StateHasChanged();
        }

        public DataTableText<TItem> Text { get; private set; }
        public void UpdateText(DataTableText<TItem> text)
        {
            Text = text;
            StateHasChanged();
        }
    }
}
