using FMFT.Web.Client.Services.Views.Accounts;
using FMFT.Web.Client.Views.Bases.Buttons;
using FMFT.Web.Client.Views.Bases.Inputs;
using FMFT.Web.Shared.Models.Users.Exceptions;
using FMFT.Web.Shared.Models.Users.Models;
using Microsoft.AspNetCore.Components;

namespace FMFT.Web.Client.Views.Components.Accounts
{
    public partial class SignInUserWithPasswordComponent
    {
        [Inject]
        public IAccountViewService AccountViewService { get; set; }

        public SignInUserWithPasswordModel Model { get; set; } = new SignInUserWithPasswordModel();

        public ButtonBase SubmitButton { get; set; }
        public TextInputBase EmailInput { get; set; }
        public TextInputBase PasswordInput { get; set; }

        public async Task SubmitLoginAsync()
        {
            SubmitButton.StartSpinning();
            try
            {
                await AccountViewService.LoginAsync(Model);
            } catch (UserPasswordNotMatchException)
            {
                // Password or username is invalid
            } finally
            {
                SubmitButton.StopSpinning();
            }
        }
    }
}
