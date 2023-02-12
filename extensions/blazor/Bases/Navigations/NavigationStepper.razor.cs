namespace FMFT.Extensions.Blazor.Bases.Navigations
{
    public partial class NavigationStepper
    {
        public async Task StepUpAsync()
        {
            int index = Items.IndexOf(ActiveNavigationItem);
            if (index >= Items.Count - 1)
            {
                // Already last item in the list
                return;
            }

            NavigationItem navigationItem = Items[index + 1];
            await StepAsync(navigationItem);
        }

        public async Task StepDownAsync()
        {
            int index = Items.IndexOf(ActiveNavigationItem);
            if (index <= 0)
            {
                // Already last item in the list
                return;
            }

            NavigationItem navigationItem = Items[index - 1];
            await StepAsync(navigationItem);
        }

        public async Task StepAsync(NavigationItem navigationItem)
        {
            await OnNavigate.InvokeAsync(navigationItem);
        }

        private async Task HandleClickAsync(NavigationItem navigationItem)
        {
            if (!IsPast(navigationItem))
            {
                return;
            }

            await OnNavigate.InvokeAsync(navigationItem);
        }

        private bool isDisabled = false;

        private bool IsPast(NavigationItem navigationItem)
        {
            int activeIndex = Items.IndexOf(ActiveNavigationItem);
            int index = Items.IndexOf(navigationItem);

            return activeIndex > index;
        }

        private bool IsActive(NavigationItem navigationItem)
        {
            return ActiveNavigationItem == navigationItem;
        }

        private string GetClasses(NavigationItem navigationItem)
        {
            List<string> classes = new();

            bool isPast = IsPast(navigationItem);

            if (IsActive(navigationItem))
            {
                classes.Add("border-2");
                classes.Add("border-dark");
                classes.Add("fw-bold");
            }
            else
            {
                if (isPast)
                {
                    classes.Add("border-dark");
                }
                else
                {
                    classes.Add("text-muted");
                }
            }

            if (isDisabled)
            {
                classes.Add("disabled");
                classes.Add("text-muted");
            }
            else
            {
                if (isPast)
                {
                    classes.Add("text-black");
                    classes.Add("cursor-pointer");
                }
            }

            return string.Join(' ', classes);
        }
    }
}
