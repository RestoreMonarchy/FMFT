﻿using FMFT.Web.Client.Services.Views.Accounts;
using FMFT.Web.Client.Views.Bases.Alerts;
using FMFT.Web.Client.Views.Bases.Buttons;
using FMFT.Web.Client.Views.Bases.Forms;
using FMFT.Web.Client.Views.Bases.Inputs;
using FMFT.Web.Server.Models.Users.Exceptions;
using FMFT.Web.Server.Models.Users.Models;
using Microsoft.AspNetCore.Components;

namespace FMFT.Web.Client.Views.Components.Accounts.Forms
{
    public partial class SignInUserWithPasswordForm
    {
        [Inject]
        public IAccountViewService AccountViewService { get; set; }

        public SignInUserWithPasswordModel Model { get; set; } = new();

        public FormBase Form { get; set; }
        public ButtonBase SubmitButton { get; set; }
        public TextInputBase EmailInput { get; set; }
        public TextInputBase PasswordInput { get; set; }
        public CheckboxInputBase PersistentCheckbox { get; set; }

        public AlertGroupBase AlertGroup { get; set; }
        public AlertBase UserPasswordNotMatchAlert { get; set; }

        public async Task SubmitLoginAsync()
        {
            AlertGroup.HideAll();
            Form.DisableAll();
            SubmitButton.StartSpinning();

            try
            {
                await Task.Delay(2000);
                await AccountViewService.LoginAsync(Model);
                AccountViewService.ForceLoadNavigateTo("/");
            } catch (UserPasswordNotMatchException)
            {
                UserPasswordNotMatchAlert.Show();
            } finally
            {
                SubmitButton.StopSpinning();
                Form.EnableAll();
            }
        }
    }
}