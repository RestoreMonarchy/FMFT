namespace FMFT.Extensions.Blazor.Bases.Inputs
{
    public partial class PasswordInputBase
    {
        private bool showPassword;

        private void HandleToggle()
        {
            showPassword = !showPassword;
            InvokeAsync(StateHasChanged);
        }

        private string GetInputType()
        {
            return showPassword ? "text" : "password";
        }
    }
}
