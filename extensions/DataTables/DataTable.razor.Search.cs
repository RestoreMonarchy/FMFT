using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace FMFT.Extensions.DataTables
{
    public partial class DataTable<TItem>
    {
        private string searchString = string.Empty;
        private System.Timers.Timer searchTimer;

        private bool isLoading = true;
        private void OnInputSearch(ChangeEventArgs args)
        {
            searchTimer.Stop();
            isLoading = true;
            searchString = args.Value.ToString();
            StateHasChanged();
            searchTimer.Start();
        }

        private void OnSearchTimeElapsed(object sender, ElapsedEventArgs e)
        {
            InvokeAsync(() =>
            {
                RefreshItems();
                StateHasChanged();
            });
        }

        public void ApplySearch(ref IEnumerable<TItem> data)
        {
            if (string.IsNullOrEmpty(searchString))
                return;

            IEnumerable<DataTableColumn<TItem>> columns = Columns.Where(x => x.IsSearchable);

            data = data.Where(i => columns.Any(c => c.ValidateSearch(i, searchString)));
        }
    }
}
