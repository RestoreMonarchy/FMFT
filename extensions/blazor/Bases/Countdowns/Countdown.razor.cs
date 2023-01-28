using Microsoft.AspNetCore.Components;
using System.Timers;
using Timer = System.Timers.Timer;

namespace FMFT.Extensions.Blazor.Bases.Countdowns
{
    public partial class Countdown
    {
        [Parameter]
        public DateTime TargetDate { get; set; }
        [Parameter]
        public string Class { get; set; }
        [Parameter]
        public EventCallback OnTimeElapsed { get; set; }

        public TimeSpan TimeLeft { get; set; }
        public string TimeLeftString => TimeLeft.ToString(@"mm\:ss");

        public Timer Timer { get; set; }

        protected override void OnParametersSet()
        {
            Timer = new Timer(1000);
            Timer.AutoReset = true;
            Timer.Elapsed += OnElapsed;
            Timer.Enabled = true;

            UpdateTimeLeft();            
        }

        private void OnElapsed(object sender, ElapsedEventArgs e)
        {
            UpdateTimeLeft();
            StateHasChanged();
        }

        private void UpdateTimeLeft()
        {
            TimeLeft = TargetDate - DateTime.Now;
            if (TimeLeft <= TimeSpan.Zero)
            {
                TimeLeft = TimeSpan.Zero;
                Timer.Enabled = false;
                Timer.Dispose();
                InvokeAsync(async () =>
                {
                    if (OnTimeElapsed.HasDelegate)
                    {
                        await OnTimeElapsed.InvokeAsync();
                    }                    
                });
            }
        }
    }
}
