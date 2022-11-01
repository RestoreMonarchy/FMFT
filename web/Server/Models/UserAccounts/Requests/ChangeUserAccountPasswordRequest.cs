namespace FMFT.Web.Server.Models.UserAccounts.Requests
{
    public class ChangeUserAccountPasswordRequest
    {
        public string CurrentPasswordText { get; set; }
        public string PasswordText { get; set; }
    }
}
