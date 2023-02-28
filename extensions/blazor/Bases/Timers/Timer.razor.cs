using Microsoft.AspNetCore.Components;
using System.Timers;

namespace FMFT.Extensions.Blazor.Bases.Timers
{
    public partial class Timer
    {
        [Parameter]
        public string Class { get; set; }

        private System.Timers.Timer timer;
        private TimeSpan displayTime;
        private string DisplayTimeString => displayTime.ToString(@"mm\:ss");

        protected override void OnInitialized()
        {
            displayTime = TimeSpan.Zero;
            timer = new(1000);
            timer.AutoReset = true;
            timer.Elapsed += OnElapsed;
            timer.Enabled = true;
        }

        private void OnElapsed(object sender, ElapsedEventArgs e)
        {
            displayTime = displayTime + TimeSpan.FromSeconds(1);
            StateHasChanged();
        }
    }
}
