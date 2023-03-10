using Microsoft.AspNetCore.Components;

namespace FMFT.Extensions.Blazor.Bases.Alerts
{
    public partial class AlertGroupBase
    {
        [Parameter]
        public RenderFragment ChildContent { get; set; }

        public ElementReference ElementReference { get; set; }

        public List<AlertBase> Alerts { get; set; } = new List<AlertBase>();
        public void AddAlert(AlertBase alertBase)
        {
            Alerts.Add(alertBase);
        }

        public void ShowAll()
        {
            foreach (AlertBase alert in Alerts)
            {
                alert.Show();
            }
        }

        public void HideAll()
        {
            foreach (AlertBase alert in Alerts)
            {
                alert.Hide();
            }
        }

        public async Task FocusAsync()
        {
            await ElementReference.FocusAsync(false);
        }
    }
}
