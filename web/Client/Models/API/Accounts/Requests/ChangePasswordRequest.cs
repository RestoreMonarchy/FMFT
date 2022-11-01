namespace FMFT.Web.Client.Models.API.Accounts.Requests
{
    public class ChangePasswordRequest
    {
        public string CurrentPasswordText { get; set; }
        public string PasswordText { get; set; }
    }
}
